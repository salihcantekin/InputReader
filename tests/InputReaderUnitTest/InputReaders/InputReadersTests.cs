﻿using InputReader;
using InputReader.Converters;
using InputReader.InputReaders.BaseInputReaders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputReaderUnitTest.InputReaders;
internal class InputReadersTests
{
    public InputReadersTests()
    {

    }

    [Test]
    public void Execute_WithValidInt_ShouldReturnInt()
    {
        // Arrange 
        var expectedValue = 10;
        var defaultConverter = new DefaultValueConverter<int>(message =>
        {
            return expectedValue;
        });

        var converterItem = new ValueConverterQueueItem<int>(defaultConverter);

        var previousResult = QueueItemResult.FromResult(1, null);
        previousResult.AddOutputParam("line", expectedValue);

        // Action
        var result = converterItem.Execute(previousResult);

        // Assert
        Assert.That((int)result.Result, Is.EqualTo(expectedValue));
        Assert.That(result.IsFailed, Is.False);
    }

    [Test]
    public void Execute_WithInvalidInt_ShouldReturnFailed()
    {
        // Arrange 
        var defaultConverter = new DefaultValueConverter<int>();

        var converterItem = new ValueConverterQueueItem<int>(defaultConverter);

        var previousResult = QueueItemResult.FromResult(1, null);
        previousResult.AddOutputParam("line", "Invalid");

        // Action
        var result = converterItem.Execute(previousResult);

        // Assert
        Assert.That(result.IsFailed, Is.True);
    }

    [Test]
    public void Execute_WithValidString_ShouldReturnString()
    {
        // Arrange 
        var expectedValue = "Test";
        var defaultConverter = new DefaultValueConverter<string>(message =>
        {
            return expectedValue;
        });

        var converterItem = new ValueConverterQueueItem<string>(defaultConverter);

        var previousResult = QueueItemResult.FromResult(1, null);
        previousResult.AddOutputParam("line", expectedValue);

        // Action
        var result = converterItem.Execute(previousResult);

        // Assert
        Assert.That((string)result.Result, Is.EqualTo(expectedValue));
        Assert.That(result.IsFailed, Is.False);
    }
}