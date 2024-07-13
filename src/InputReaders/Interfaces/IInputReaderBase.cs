using System;

namespace InputReader.InputReaders.Interfaces;

public interface IInputReaderBase
{
    string ReadLine();
    ConsoleKeyInfo ReadKey(bool intercept = false);
    ConsoleKeyInfo ReadKey();

    //static string ReadLine() => Console.ReadLine();
    //static ConsoleKeyInfo ReadKey() => Console.ReadKey();
}


public interface IInputReaderBase<TInputType, out TCustomInputValueType> //: IInputReaderBase
    where TCustomInputValueType : InputValue<TInputType>
{
    TCustomInputValueType Read();
}