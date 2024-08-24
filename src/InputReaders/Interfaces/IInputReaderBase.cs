namespace InputReader.InputReaders.Interfaces;

public interface IInputReaderBase
{
    object Read();

    //string ReadLine();
    //ConsoleKeyInfo ReadKey(bool intercept = false);
    //ConsoleKeyInfo ReadKey();

    //static string ReadLine() => Console.ReadLine();
    //static ConsoleKeyInfo ReadKey() => Console.ReadKey();
}


public interface IInputReaderBase<TInputType, out TInputValueType> //: IInputReaderBase
    where TInputValueType : InputValue<TInputType>
{
    TInputValueType Read();
}