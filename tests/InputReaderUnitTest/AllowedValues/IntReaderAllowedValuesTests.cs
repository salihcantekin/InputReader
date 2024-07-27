﻿using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using Moq;
using System;

namespace InputReaderUnitTest.AllowedValues;
internal class IntReaderAllowedValuesTests
{
    private Mock<IInputReaderBase> mockReader;

    public IntReaderAllowedValuesTests()
    {
        mockReader = new Mock<IInputReaderBase>();
    }

    [Test]
    [TestCase("1", ExpectedResult = true)]
    [TestCase("5", ExpectedResult = false)]
    public bool Read_WithAllowedValue_ShouldReturnExpectedIsValid(string input)
    {
        // Arrange
        ConfigureMockReader(input);

        var reader = BuildIntReader().WithAllowedValues(1, 2);

        // Action
        var value = reader.Read();

        // return
        return value.IsValid;
    }

    [Test]
    [TestCase("1", ExpectedResult = 1)]
    [TestCase("2", ExpectedResult = 2)]
    [TestCase("3", ExpectedResult = null)]
    public int? Read_WithParamsAllowedValue_ShouldReturnExpectedValue(string input)
    {
        // Arrange
        ConfigureMockReader(input);
        var reader = BuildIntReader().WithAllowedValues(1, 2);

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    [Test]
    [TestCase("1", ExpectedResult = 1)]
    [TestCase("2", ExpectedResult = 2)]
    [TestCase("3", ExpectedResult = null)]
    public int? Read_WithStringEnumerableAllowedValue_ShouldReturnExpectedValue(string input)
    {
        // Arrange
        ConfigureMockReader(input);
        IEnumerable<string> enumerableValues = ["1", "2"];
        var reader = BuildIntReader().WithAllowedValues(enumerableValues);

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }


    [Test]
    [TestCase("1", ExpectedResult = 1)]
    [TestCase("2", ExpectedResult = 2)]
    [TestCase("3", ExpectedResult = null)]
    public int? Read_WithIntEnumerableAllowedValue_ShouldReturnExpectedValue(string input)
    {
        // Arrange
        ConfigureMockReader(input);
        IEnumerable<int?> enumerableValues = [1, 2];
        var reader = BuildIntReader().WithAllowedValues(enumerableValues);

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    [Test]
    [TestCase("1", 1, 10, ExpectedResult = 1)]
    [TestCase("5", 1, 10, ExpectedResult = 5)]
    [TestCase("10", 1, 10, ExpectedResult = 10)]
    [TestCase("1", 5, 10, ExpectedResult = null)]
    public int? Read_WithRangeAllowedValue_ShouldReturnExpectedValue(string input, int rangeStart, int rangeEnd)
    {
        // Arrange
        ConfigureMockReader(input);
        var range = new System.Range(rangeStart, rangeEnd);
        var reader = BuildIntReader().WithAllowedValues(range);

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    #region Private Methods

    private IntInputReader BuildIntReader()
    {
        return (IntInputReader)new IntInputReader().With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine)
    {
        mockReader.Setup(i => i.ReadLine()).Returns(readLine);
    }

    #endregion
}