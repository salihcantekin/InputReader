using InputReader.Converters;

namespace InputReaderUnitTest.Converter.IntInputConverter;
internal class IntConverterTests
{
    private DefaultValueConverter<int> converter;

    [SetUp]
    public void SetUp()
    {
        converter = new DefaultValueConverter<int>();
    }

    [Test]
    public void TryConvertFromString_WhenInputIsValidInt_ShouldReturnInt()
    {
        // Arrange
        var defaultValueString = "10";
        var expectedDefaultValue = 10;

        // Act
        var success = converter.TryConvertFromString(defaultValueString, out var result);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(success, Is.True);
        });

        Assert.That(result, Is.EqualTo(expectedDefaultValue));
    }

    [Test]
    public void TryConvertFromString_WhenInputIsInvalidInt_ShouldReturnFalse()
    {
        // Arrange
        var defaultValueString = "10.5";

        // Act
        var success = converter.TryConvertFromString(defaultValueString, out var result);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(success, Is.False);
        });

        Assert.That(result, Is.EqualTo(default(int)));
    }

    [Test]
    public void TryConvertFromString_WhenInputIsNotInt_ShouldReturnFalse()
    {
        // Arrange
        var defaultValueString = "ten";

        // Act
        var success = converter.TryConvertFromString(defaultValueString, out var result);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(success, Is.False);
        });

        Assert.That(result, Is.EqualTo(default(int)));
    }
}
