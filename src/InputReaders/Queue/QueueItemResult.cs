using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace InputReader.InputReaders.Queue;

internal record QueueItemResult
{
    private QueueItemResult() { }

    public bool IsFailed { get; set; }

    internal QueueItemResult PreviousItemResult { get; set; }

    private Dictionary<string, object> OutputParams { get; set; } = new();

    internal object Result { get; set; }

    internal void AddOutputParam(string key, object value)
    {
        OutputParams ??= [];

        OutputParams[key] = value;
    }

    internal object GetOutputParam(string key)
    {
        return OutputParams[key];
    }


    public static QueueItemResult FromResult(object result, QueueItemResult previousItemResult)
    {
        return new QueueItemResult
        {
            Result = result,
            PreviousItemResult = previousItemResult,
            OutputParams = previousItemResult?.OutputParams
        };
    }

    internal static QueueItemResult Failed() => new() { IsFailed = true };
}
