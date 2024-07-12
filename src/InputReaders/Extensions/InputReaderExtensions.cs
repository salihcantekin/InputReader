using InputReader.InputReaders.Interfaces;
using System;

namespace InputReader.InputReaders.Extensions;

public static partial class InputReaderExtensions
{
    public static TCustomInputValueType ReadUntilValid<TInputType, TCustomInputValueType>(
        this IInputReader<TInputType, TCustomInputValueType> reader)
        where TCustomInputValueType : InputValue<TInputType>
    {
        TCustomInputValueType result;

        do
        {
            result = reader.Read();
            if (!result.IsValid)
                Console.WriteLine("Geçersiz değer girdiniz! \n"); //Kullanıcı hatalı giriş yapınca bilgilendirilebilir

        } while (!result.IsValid);

        return result;
    }

    public static TCustomInputValueType ReadUntil<TInputType, TCustomInputValueType>(
        this IInputReader<TInputType, TCustomInputValueType> reader, Func<TCustomInputValueType, bool> valuePredicate)
        where TCustomInputValueType : InputValue<TInputType>
    {
        do
        {
            var input = reader.Read();

            if (valuePredicate(input))
            {
                return input;
            }

        } while (true);
    }

    public static TCustomInputValueType ReadUntilValidEmail<TInputType, TCustomInputValueType>(
        this IInputReader<TInputType, TCustomInputValueType> reader)
        where TCustomInputValueType : InputValue<TInputType>
    {
        return reader.ReadUntil(input =>
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(input.Value.ToString());
                
                return true;
            }
            catch
            {
                return false;
            }
        });
    }
}