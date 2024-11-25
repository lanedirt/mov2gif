using Microsoft.AspNetCore.Components.Forms;

namespace Mov2Gif.Client.Models;

/// <summary>
/// Contains settings for the GIF conversion process.
/// </summary>
public class ConversionSettings
{
    public bool IsConverting { get; set; }
    public string? OutputGifUrl { get; set; }
    public MemoryStream? OutputGifStream { get; set; }
    public int Quality { get; set; } = 50;
    public int FrameRate { get; set; } = 15;
    public IBrowserFile? CurrentFile { get; set; }
    public bool IsDragging { get; set; }
    public string? InputFileSize { get; set; }
    public string? OutputFileSize { get; set; }
    public int Resolution { get; set; } = 480;
    
    /// <summary>
    /// Formats the given file size in bytes to a human-readable format.
    /// </summary>
    /// <param name="bytes">Amount of bytes.</param>
    /// <returns></returns>
    public static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        int order = 0;
        double size = bytes;

        while (size >= 1024 && order < sizes.Length - 1)
        {
            order++;
            size /= 1024;
        }

        return $"{size:0.##} {sizes[order]}";
    }
    
    /// <summary>
    /// Resets all settings to their default values.
    /// </summary>
    public void Reset()
    {
        IsConverting = false;
        OutputGifUrl = null;
        OutputGifStream = null;
        Quality = 50;
        FrameRate = 15;
        CurrentFile = null;
        IsDragging = false;
        InputFileSize = null;
        OutputFileSize = null;
        Resolution = 480;
    }
}