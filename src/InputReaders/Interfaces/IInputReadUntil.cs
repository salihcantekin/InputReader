namespace InputReader.InputReaders.Interfaces;

public interface IInputReadUntil<T, out TResultType> where TResultType : InputValue<T>
{

}