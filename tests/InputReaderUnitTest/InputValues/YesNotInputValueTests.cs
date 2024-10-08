﻿using FluentAssertions;
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
        charInputValue.Value.Should().Be(expected);
    }

    [Test]
    public void Value_DefaultIsValidValue_ShouldBeFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('s');

        // Assert
        yesNoInputValue.IsValid.Should().BeFalse();
    }

    [Test]
    public void CharInput_ShouldBeImplicitlyNullableChar()
    {
        // Arrange
        var expectedChar = 's';
        var yesNoInputValue = new YesNoInputValue(expectedChar);

        // Act
        char? charValue = yesNoInputValue;

        // Assert
        charValue.Should().Be(expectedChar);
    }

    [Test]
    public void IsYes_WhenInputLowerYes_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('y');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        isYes.Should().BeTrue();
    }

    [Test]
    public void IsYes_WhenInputUpperYes_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('Y');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        isYes.Should().BeTrue();
    }

    [Test]
    public void IsYes_WhenInputLowerNo_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('n');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        isYes.Should().BeFalse();
    }

    [Test]
    public void IsYes_WhenInputUpperNo_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('N');

        // Act
        bool isYes = yesNoInputValue.IsYes();

        // Assert
        isYes.Should().BeFalse();
    }

    [Test]
    public void IsNo_WhenInputLowerYes_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('y');

        // Act
        bool isYes = yesNoInputValue.IsNo();

        // Assert
        isYes.Should().BeFalse();
    }

    [Test]
    public void IsNo_WhenInputUpperYes_ShouldReturnFalse()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('Y');

        // Act
        bool isYes = yesNoInputValue.IsNo();

        // Assert
        isYes.Should().BeFalse();
    }

    [Test]
    public void IsNo_WhenInputLowerNo_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('n');

        // Act
        bool isYes = yesNoInputValue.IsNo();

        // Assert
        isYes.Should().BeTrue();
    }

    [Test]
    public void IsNo_WhenInputUpperNo_ShouldReturnTrue()
    {
        // Arrange
        var yesNoInputValue = new YesNoInputValue('N');

        // Act
        bool isYes = yesNoInputValue.IsNo();

        // Assert
        isYes.Should().BeTrue();
    }
}
