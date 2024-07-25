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
    
    [Test]
    public void IsYes_WhenInputIsNull_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue(null);

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        Assert.That(isYes, Is.False);
    }

    [Test]
    public void IsNo_WhenInputIsNull_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue(null);

        // Act
        bool isNo = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isNo, Is.False);
    }

    [Test]
    public void IsYes_WhenInputLowerIsInvalidChar_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('x');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        Assert.That(isYes, Is.False);
    }

    [Test]
    public void IsNo_WhenInputLowerIsInvalidChar_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('x');

        // Act
        bool isNo = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isNo, Is.False);
    }

    [Test]
    public void IsYes_WhenInputUpperIsInvalidChar_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('X');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        Assert.That(isYes, Is.False);
    }

    [Test]
    public void IsNo_WhenInputUpperIsInvalidChar_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('X');

        // Act
        bool isNo = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isNo, Is.False);
    }
    
    [Test]
    public void IsYes_WhenInputIsWhitespace_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue(' ');

        // Act
        bool isYes = yesNoInputValue.IsYes();
        Console.WriteLine(isYes);

        // Assert
        Assert.That(isYes, Is.False);
    }


    [Test]
    public void IsNo_WhenInputIsWhitespace_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue(' ');

        // Act
        bool isNo = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isNo, Is.False);
    }

    [Test]
    public void IsYes_WhenInputIsSpecialCharacter_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('@');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        Assert.That(isYes, Is.False);
    }

    [Test]
    public void IsNo_WhenInputIsSpecialCharacter_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('@');

        // Act
        bool isNo = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isNo, Is.False);
    }

    [Test]
    public void IsYes_WhenInputIsDigit_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('1');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        Assert.That(isYes, Is.False);
    }

    [Test]
    public void IsNo_WhenInputIsDigit_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('1');

        // Act
        bool isNo = yesNoInputValue.IsNo();

        // Assert
        Assert.That(isNo, Is.False);
    }

    [Test]
    public void Value_WhenInputValueIsNull_ShouldReturnNull()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue(null);

        // Assert
        Assert.That(yesNoInputValue.Value, Is.Null);
    }

    [Test]
    public void Value_WhenInputValueIsValidYes_ShouldReturnYes()
    {
        // Arrange
        var expected = 'Y';
        var yesNoInputValue = new YesNoInputValue(expected);

        // Assert
        Assert.That(yesNoInputValue.Value, Is.EqualTo(expected));
    }

    [Test]
    public void Value_WhenInputValueIsValidNo_ShouldReturnNo()
    {
        // Arrange
        var expected = 'n';
        var yesNoInputValue = new YesNoInputValue(expected);

        // Assert
        Assert.That(yesNoInputValue.Value, Is.EqualTo(expected));
    }

    [Test]
    public void IsValid_WhenInputValueIsYes_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('Y');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.True);
    }

    [Test]
    public void IsValid_WhenInputValueIsNo_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('n');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.True);
    }

    [Test]
    public void IsValid_WhenInputValueIsNull_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue(null);

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.False);
    }

    [Test]
    public void IsValid_WhenInputValueIsInvalidChar_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('x');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.False);
    }

    [Test]
    public void IsValid_WhenInputValueIsValidYesOrNo_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValueYes = new YesNoInputValue('Y');
        var yesNoInputValueNo = new YesNoInputValue('n');

        // Assert
        Assert.That(yesNoInputValueYes.IsValid, Is.True);
        Assert.That(yesNoInputValueNo.IsValid, Is.True);
    }

    [Test]
    public void IsValid_WhenInputValueIsSpecialChar_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('!');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.False);
    }

    [Test]
    public void IsValid_WhenInputValueIsDigit_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('1');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.False);
    }

    [Test]
    public void IsValid_WhenInputValueIsWhitespace_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue(' ');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.False);
    }

    [Test]
    public void IsValid_WhenInputLowerValueIsControlChar_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('\n');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.False);
    }
    
    [Test]
    public void IsValid_WhenInputValueIsEmpty_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('\0');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.False);
    }

    [Test]
    public void IsValid_WhenInputValueIsPunctuation_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('.');

        // Assert
        Assert.That(yesNoInputValue.IsValid, Is.False);
    }

    [Test]
    public void ToString_WhenInputValueIsNull_ShouldReturnEmptyString()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue(null);

        // Assert
        Assert.That(yesNoInputValue.ToString(), Is.EqualTo(string.Empty));
    }

    [Test]
    public void ToString_WhenInputValueIsValidYes_ShouldReturnYes()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('Y');

        // Assert
        Assert.That(yesNoInputValue.ToString(), Is.EqualTo("Y"));
    }

    [Test]
    public void ToString_WhenInputValueIsValidNo_ShouldReturnNo()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('n');

        // Assert
        Assert.That(yesNoInputValue.ToString(), Is.EqualTo("n"));
    }

    [Test]
    public void ImplicitConversion_FromYesNoInputValueToChar_ShouldReturnChar()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('n');

        // Act
        char? charValue = yesNoInputValue;

        // Assert
        Assert.That(charValue, Is.EqualTo('n'));
    }

    [Test]
    public void Equals_WhenInputValuesAreEqual_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue1 = new YesNoInputValue('Y');
        var yesNoInputValue2 = new YesNoInputValue('Y');

        // Assert
        Assert.That(yesNoInputValue1.Equals(yesNoInputValue2), Is.True);
    }

    [Test]
    public void Equals_WhenInputValuesAreNotEqual_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue1 = new YesNoInputValue('Y');
        var yesNoInputValue2 = new YesNoInputValue('n');

        // Assert
        Assert.That(yesNoInputValue1.Equals(yesNoInputValue2), Is.False);
    }
    
    [Test]
    public void GetHashCode_WhenInputValuesAreEqual_ShouldReturnSameHashCode()
    {
        // Arrange
        var yesNoInputValue1 = new YesNoInputValue('Y');
        var yesNoInputValue2 = new YesNoInputValue('Y');

        // Assert
        Assert.That(yesNoInputValue1.GetHashCode(), Is.EqualTo(yesNoInputValue2.GetHashCode()));
    }
    
    [Test]
    public void GetHashCode_WhenInputValuesAreNotEqual_ShouldReturnDifferentHashCode()
    {
        // Arrange
        var yesNoInputValue1 = new YesNoInputValue('Y');
        var yesNoInputValue2 = new YesNoInputValue('n');

        // Assert
        Assert.That(yesNoInputValue1.GetHashCode(), Is.Not.EqualTo(yesNoInputValue2.GetHashCode()));
    }
    
    [Test]
    public void GetType_WhenGetType_ShouldReturnYesNoInputValue()
    {
        // Arrange
        var yesNoInputValue1 = new YesNoInputValue('Y');

        // Assert
        Assert.That(yesNoInputValue1.GetType(), Is.EqualTo(typeof(YesNoInputValue)));
    }
    
    [Test]
    public void GetType_WhenGetType_ShouldNotReturnCharInputValue()
    {
        // Arrange
        var yesNoInputValue1 = new YesNoInputValue('Y');

        // Assert
        Assert.That(yesNoInputValue1.GetType(), Is.Not.EqualTo(typeof(CharInputValue)));
    }
    
    [Test]
    public void GetType_WhenGetType_ShouldNotReturnInputValue()
    {
        // Arrange
        var yesNoInputValue1 = new YesNoInputValue('Y');

        // Assert
        Assert.That(yesNoInputValue1.GetType(), Is.Not.EqualTo(typeof(InputValue<char?>)));
    }
}
