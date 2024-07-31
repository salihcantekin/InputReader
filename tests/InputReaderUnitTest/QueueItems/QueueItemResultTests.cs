using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using InputReader.InputReaders.Queue;

namespace InputReader.Tests.InputReaders.Queue;

public class QueueItemResultTests
{
    [Test]
    public void FromResult_ShouldInitializeCorrectly()
    {
        // Arrange
        var previousResult = QueueItemResult.FromResult(new object(), null);
        var result = new object();

        // Act
        var queueItemResult = QueueItemResult.FromResult(result, previousResult);

        // Assert
        queueItemResult.Result.Should().Be(result);
        queueItemResult.PreviousItemResult.Should().Be(previousResult);
        queueItemResult.OutputParams.Should().BeSameAs(previousResult.OutputParams);
    }

    [Test]
    public void Failed_ShouldSetIsFailedToTrue()
    {
        // Act
        var queueItemResult = QueueItemResult.Failed();

        // Assert
        queueItemResult.IsFailed.Should().BeTrue();
    }

    [Test]
    public void AddOutputParam_ShouldAddParameter()
    {
        // Arrange
        var queueItemResult = QueueItemResult.FromResult(new object(), null);
        var key = "testKey";
        var value = new object();

        // Act
        queueItemResult.AddOutputParam(key, value);

        // Assert
        queueItemResult.GetOutputParam(key).Should().Be(value);
    }

    [Test]
    public void GetOutputParam_ShouldReturnCorrectValue()
    {
        // Arrange
        var queueItemResult = QueueItemResult.FromResult(new object(), null);
        var key = "testKey";
        var value = new object();
        queueItemResult.AddOutputParam(key, value);

        // Act
        var result = queueItemResult.GetOutputParam(key);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void GetOutputParam_Generic_ShouldReturnCorrectValue()
    {
        // Arrange
        var queueItemResult = QueueItemResult.FromResult(new object(), null);
        var key = "testKey";
        var value = "testValue";
        queueItemResult.AddOutputParam(key, value);

        // Act
        var result = queueItemResult.GetOutputParam<string>(key);

        // Assert
        result.Should().Be(value);
    }
}
