using InputReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputReaderUnitTest.InputValues;

internal class IntInputValueTests
{
    [Test]
    public void Value_WhenInputIsValidInt_ShouldReturnInt()
    {
        // Arrange
        var expectedInt = 123;
        var intInputValue = new IntInputValue(expectedInt);

        // Assert
        Assert.That(intInputValue.Value, Is.EqualTo(expectedInt));
    }

    [Test]
    public void Value_DefaultIsValidValue_ShouldBeFalse()
    {
        // Arrange
        var expectedInt = 123;
        var intInputValue = new IntInputValue(expectedInt);

        // Assert
        Assert.That(intInputValue.IsValid, Is.False);
    }

    [Test]
    public void IntInput_ShouldBeImplicitlyNullableInt()
    {
        // Arrange
        var expectedInt = 123;
        IntInputValue intInputValue = new IntInputValue(expectedInt);

        // Act
        int? intValue = intInputValue;

        // Assert
        Assert.That(intValue, Is.EqualTo(expectedInt));
    }

    [Test]
    public void Is_WithValidInt_ShouldReturnTrue()
    {
        // Arrange
        var expectedInt = 123;
        var intInputValue = new IntInputValue(expectedInt);

        // Act
        var result = intInputValue.Is(expectedInt);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Is_WithInvalidInt_ShouldReturnFalse()
    {
        // Arrange
        var expectedInt = 123;
        var intInputValue = new IntInputValue(expectedInt);

        // Act
        var result = intInputValue.Is(321);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsZero_WithZeroValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(0);

        // Act
        var result = intInputValue.IsZero();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsZero_WithNonZeroValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(1);

        // Act
        var result = intInputValue.IsZero();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOne_WithOneValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(1);

        // Act
        var result = intInputValue.IsOne();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOne_WithNonOneValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(2);

        // Act
        var result = intInputValue.IsOne();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsTwo_WithTwoValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(2);

        // Act
        var result = intInputValue.IsTwo();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsTwo_WithNonTwoValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(3);

        // Act
        var result = intInputValue.IsTwo();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsThree_WithThreeValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(3);

        // Act
        var result = intInputValue.IsThree();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsThree_WithNonThreeValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(4);

        // Act
        var result = intInputValue.IsThree();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsFour_WithFourValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(4);

        // Act
        var result = intInputValue.IsFour();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsFour_WithNonFourValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsFour();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsFive_WithFiveValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsFive();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsFive_WithNonFiveValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(6);

        // Act
        var result = intInputValue.IsFive();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGreaterThan_WithGreaterValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsGreaterThan(4);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsGreaterThan_WithEqualValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsGreaterThan(5);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGreaterThan_WithLessValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsGreaterThan(6);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsLessThan_WithLessValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsLessThan(6);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsLessThan_WithEqualValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsLessThan(5);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsLessThan_WithGreaterValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsLessThan(4);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGreaterThanOrEqualTo_WithGreaterValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsGreaterThanOrEqualTo(4);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsGreaterThanOrEqualTo_WithEqualValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsGreaterThanOrEqualTo(5);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsGreaterThanOrEqualTo_WithLessValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsGreaterThanOrEqualTo(6);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsLessThanOrEqualTo_WithLessValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsLessThanOrEqualTo(6);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsLessThanOrEqualTo_WithEqualValue_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsLessThanOrEqualTo(5);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsLessThanOrEqualTo_WithGreaterValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.IsLessThanOrEqualTo(4);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Is_WithNegativeInt_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(-1);

        // Assert
        Assert.That(intInputValue.Is(-1), Is.True);
    }

    [Test]
    public void Is_WithNegativeInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(-1);

        // Assert
        Assert.That(intInputValue.Is(-2), Is.False);
    }

    [Test]
    public void Is_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.Is(0);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsZero_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsZero();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOne_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsOne();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsTwo_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsTwo();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsThree_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsThree();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsFour_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsFour();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsFive_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsFive();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGreaterThan_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsGreaterThan(0);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsLessThan_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsLessThan(0);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGreaterThanOrEqualTo_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsGreaterThanOrEqualTo(0);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsLessThanOrEqualTo_WithNullValue_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(null);

        // Act
        var result = intInputValue.IsLessThanOrEqualTo(0);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Is_WithLargeInt_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.Is(int.MaxValue);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Is_WithLargeInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.Is(int.MaxValue - 1);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Is_WithSmallInt_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MinValue);

        // Act
        var result = intInputValue.Is(int.MinValue);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Is_WithSmallInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MinValue);

        // Act
        var result = intInputValue.Is(int.MinValue + 1);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsZero_WithLargeInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsZero();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOne_WithLargeInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsOne();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsTwo_WithLargeInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsTwo();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsThree_WithLargeInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsThree();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsFour_WithLargeInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsFour();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsFive_WithLargeInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsFive();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGreaterThan_WithLargeInt_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsGreaterThan(int.MaxValue);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsLessThan_WithLargeInt_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsLessThan(int.MaxValue);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGreaterThanOrEqualTo_WithLargeInt_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsGreaterThanOrEqualTo(int.MaxValue);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsLessThanOrEqualTo_WithLargeInt_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(int.MaxValue);

        // Act
        var result = intInputValue.IsLessThanOrEqualTo(int.MaxValue);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Is_WithCombination_ShouldReturnTrue()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.Is(5) && intInputValue.IsGreaterThan(4) && intInputValue.IsLessThan(6);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Is_WithCombination_ShouldReturnFalse()
    {
        // Arrange
        var intInputValue = new IntInputValue(5);

        // Act
        var result = intInputValue.Is(5) && intInputValue.IsGreaterThan(6) && intInputValue.IsLessThan(4);

        // Assert
        Assert.That(result, Is.False);
    }
}