using FFMpegCore;
using FFMpegCore.Pipes;

namespace Mov2Gif.Utilities.GifConverter;

public class MovToGifConverter
{
    public async Task<Stream> ConvertToGif(Stream movStream, int frameRate = 30, int quality = 85, int maxResolution = 480)
    {
        // Create a temporary file for the input MOV
        var tempInputPath = Path.GetTempFileName() + ".mov";
        var tempOutputPath = Path.GetTempFileName() + ".gif";

        try
        {
            // Save the input stream to a temporary file
            using (var fileStream = File.Create(tempInputPath))
            {
                await movStream.CopyToAsync(fileStream);
            }

            // Calculate dimensions while maintaining aspect ratio
            var mediaInfo = await FFProbe.AnalyseAsync(tempInputPath);
            var width = mediaInfo.VideoStreams.First().Width;
            var height = mediaInfo.VideoStreams.First().Height;
            var maxDimension = (double)maxResolution;

            var scale = Math.Min(maxDimension / width, maxDimension / height);
            var newWidth = (int)(width * scale);
            var newHeight = (int)(height * scale);

            // Ensure even dimensions
            newWidth = newWidth + (newWidth % 2);
            newHeight = newHeight + (newHeight % 2);

            // Convert quality (0-100) to number of colors (2-256)
            var colors = Math.Max(2, Math.Min(256, (int)(quality * 2.56)));

            // Convert to GIF using FFmpeg with better quality control
            await FFMpegArguments
                .FromFileInput(tempInputPath)
                .OutputToFile(tempOutputPath, false, options => options
                    .WithCustomArgument($"-vf \"scale={newWidth}:{newHeight},split[s0][s1];[s0]palettegen=max_colors={colors}:stats_mode=full[p];[s1][p]paletteuse=dither=floyd_steinberg:diff_mode=rectangle\"")
                    .WithFramerate(frameRate)
                    .WithVideoCodec("gif")
                )
                .ProcessAsynchronously();

            // Read the output file into a memory stream
            var outputStream = new MemoryStream();
            using (var fileStream = File.OpenRead(tempOutputPath))
            {
                await fileStream.CopyToAsync(outputStream);
            }

            outputStream.Position = 0;
            return outputStream;
        }
        finally
        {
            // Clean up temporary files
            if (File.Exists(tempInputPath))
                File.Delete(tempInputPath);
            if (File.Exists(tempOutputPath))
                File.Delete(tempOutputPath);
        }
    }
}