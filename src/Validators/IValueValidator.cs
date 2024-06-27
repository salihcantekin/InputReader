namespace InputReader.Validators;

public interface IValidator<in T>
{
    bool IsValid(T value);
}

public interface IPreValidator : IValidator<string>
{
}

public interface IPostValidator<in T> : IValidator<T>
{

}