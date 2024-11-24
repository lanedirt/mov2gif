using FFMpegCore;
using FFMpegCore.Pipes;

namespace Mov2Gif.Utilities.GifConverter;

public class MovToGifConverter
{
    public async Task<Stream> ConvertToGif(Stream movStream, int frameRate = 30, int quality = 85)
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
            var maxDimension = 480d; // Max width or height

            var scale = Math.Min(maxDimension / width, maxDimension / height);
            var newWidth = (int)(width * scale);
            var newHeight = (int)(height * scale);

            // Ensure even dimensions
            newWidth = newWidth + (newWidth % 2);
            newHeight = newHeight + (newHeight % 2);

            // Convert to GIF using FFmpeg
            await FFMpegArguments
                .FromFileInput(tempInputPath)
                .OutputToFile(tempOutputPath, false, options => options
                    .WithCustomArgument($"-vf \"scale={newWidth}:{newHeight},split[s0][s1];[s0]palettegen[p];[s1][p]paletteuse\"")
                    .WithFramerate(frameRate)
                    .WithVideoCodec("gif")
                    .WithCustomArgument($"-quality {quality}")
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