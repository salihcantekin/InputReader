using InputReader.Converters;
using InputReader.Converters.CustomConverters;

namespace InputReaderUnitTest.Converters;
internal class CustomTimeOnlyValueConverter
{
    private TimeOnlyValueConverter converter;

    [SetUp]
    public void SetUp()
    {
        converter = new TimeOnlyValueConverter();
    }

    [Test]
    public void TryConvertFromString_WhenInputIsValidTimeOnlyString_ShouldReturnTimeOnly()
    {
        // Arrange
        var timeOnlyString = "23:59:59";
        var expectedTimeOnly = new CustomTimeOnly(23, 59, 59);

        // Act
        var success = converter.TryConvert(timeOnlyString, out var result);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.EqualTo(expectedTimeOnly));
    }

    [Test]
    public void TryConvertFromString_WhenInputIsInvalidTimeOnlyString_ShouldReturnFalse()
    {
        // Arrange
        var timeOnlyString = "24:00:00";

        // Act
        var success = converter.TryConvert(timeOnlyString, out var result);

        // Assert
        Assert.That(success, Is.False);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void TryConvertFromString_WhenInputIsNotTimeOnlyString_ShouldReturnFalse()
    {
        // Arrange
        var timeOnlyString = "23:59:59.9999999";

        // Act
        var success = converter.TryConvert(timeOnlyString, out var result);

        // Assert
        Assert.That(success, Is.False);
    }
}
