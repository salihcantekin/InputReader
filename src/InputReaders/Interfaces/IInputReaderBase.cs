using System;

namespace InputReader.InputReaders.Interfaces;

public interface IInputReaderBase
{
    static string ReadLine() => Console.ReadLine();
    static ConsoleKeyInfo ReadKey() => Console.ReadKey();
}


public interface IInputReaderBase<TInputType, out TCustomInputValueType> : IInputReaderBase
    where TCustomInputValueType : InputValue<TInputType>
{
    TCustomInputValueType Read();
}