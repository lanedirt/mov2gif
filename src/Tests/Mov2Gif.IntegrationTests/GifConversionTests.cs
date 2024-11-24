using Mov2Gif.Utilities.GifConverter;

namespace Mov2Gif.IntegrationTests;

public class GifConversionTests
{
    private MovToGifConverter _converter;
    private const string TestDataNamespace = "Mov2Gif.IntegrationTests.TestData";

    [SetUp]
    public void Setup()
    {
        _converter = new MovToGifConverter();
    }


    [Test]
    public async Task Should_ProduceValidGif_When_ConvertingRickrollMov()
    {
        // Arrange
        await using var movStream = typeof(GifConversionTests).Assembly
            .GetManifestResourceStream($"{TestDataNamespace}.rickroll.mov");

        Assert.That(movStream, Is.Not.Null, "Test MOV file should exist in embedded resources");

        // Act
        await using var gifStream = await _converter.ConvertToGif(movStream, frameRate: 15);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(gifStream, Is.Not.Null);
            Assert.That(gifStream.Length, Is.GreaterThan(0), "GIF file size should be greater than 0");
        });
    }

    [Test]
    public async Task Should_ProduceValidGif_When_UsingCustomOptions()
    {
        // Arrange
        await using var movStream = typeof(GifConversionTests).Assembly
            .GetManifestResourceStream($"{TestDataNamespace}.rickroll.mov");

        Assert.That(movStream, Is.Not.Null, "Test MOV file should exist in embedded resources");

        // Act
        await using var gifStream = await _converter.ConvertToGif(
            movStream,
            frameRate: 30,
            quality: 95,
            maxResolution: 720
        );

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(gifStream, Is.Not.Null);
            Assert.That(gifStream.Length, Is.GreaterThan(0), "GIF file size should be greater than 0");
        });
    }
}