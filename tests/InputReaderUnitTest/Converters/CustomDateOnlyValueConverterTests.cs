using InputReader.Converters;
using InputReader.Converters.CustomConverters;

namespace InputReaderUnitTest.Converters;
internal class CustomDateOnlyValueConverterTests
{
    private DateOnlyValueConverter converter;

    [SetUp]
    public void SetUp()
    {
        converter = new DateOnlyValueConverter();
    }

    [Test]
    public void TryConvertFromString_WhenInputIsValidDateOnlyString_ShouldReturnDateOnly()
    {
        // Arrange
        var dateOnlyString = "2021-12-31";
        var expectedDateOnly = new CustomDateOnly(2021, 12, 31);

        // Act
        var success = converter.TryConvert(dateOnlyString, out var result);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.EqualTo(expectedDateOnly));
    }

    [Test]
    public void TryConvertFromString_WhenInputIsInvalidDateOnlyString_ShouldReturnFalse()
    {
        // Arrange
        var dateOnlyString = "2021-12-32";

        // Act
        var success = converter.TryConvert(dateOnlyString, out var result);

        // Assert
        Assert.That(success, Is.False);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void TryConvertFromString_WhenInputIsNotDateOnlyString_ShouldReturnFalse()
    {
        // Arrange
        var dateOnlyString = "2021-12-31T00:00:00";

        // Act
        var success = converter.TryConvert(dateOnlyString, out var result);

        // Assert
        Assert.That(success, Is.False);
        Assert.That(result, Is.Null);
    }
}
