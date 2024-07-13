using InputReader;

namespace InputReaderUnitTest.InputValues;
internal class YesNotInputValueTests
{
    [Test]
    public void Value_WhenInputValueChar_ShouldReturnChar()
    {
        // Arrange
        var expected = 's';
        var charInputValue = new YesNoInputValue(expected);

        // Assert
        Assert.That(charInputValue.Value, Is.EqualTo(expected));
    }

    [Test]
    public void Value_DefaultIsValidValue_ShouldBeFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('s');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.False);
    }

    [Test]
    public void CharInput_ShouldBeImplicitlyNullableChar()
    {
        // Arrange
        var expectedInt = 's';
        var yesNoInputValue = new YesNoInputValue(expectedInt);

        // Act
        char? charValue = yesNoInputValue;

        // Assert
        Assert.That(charValue, Is.EqualTo(expectedInt));
    }

    [Test]
    public void IsYes_WhenInputLowerYes_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('y');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        Assert.That(isYes, Is.True);
    }

    [Test]
    public void IsYes_WhenInputUpperYes_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('Y');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        Assert.That(isYes, Is.True);
    }

    [Test]
    public void IsYes_WhenInputLowerNo_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('n');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        Assert.That(isYes, Is.False);
    }

    [Test]
    public void IsYes_WhenInputUpperNo_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('N');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        Assert.That(isYes, Is.False);
    }

    [Test]
    public void IsNo_WhenInputLowerYes_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('y');

        // Act
        bool isYes = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isYes, Is.False);
    }

    [Test]
    public void IsNo_WhenInputUpperYes_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('Y');

        // Act
        bool isYes = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isYes, Is.False);
    }

    [Test]
    public void IsNo_WhenInputLowerNo_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('n');

        // Act
        bool isYes = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isYes, Is.True);
    }

    [Test]
    public void IsNo_WhenInputUpperNo_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('N');

        // Act
        bool isYes = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isYes, Is.True);
    }

}
