### Nuget

[![](https://img.shields.io/nuget/dt/ConsoleInputReader?style=for-the-badge)](https://img.shields.io/nuget/dt/ConsoleInputReader)
[![](https://img.shields.io/nuget/v/ConsoleInputReader?style=for-the-badge)](https://img.shields.io/nuget/v/ConsoleInputReader)
[![](https://img.shields.io/nuget/vpre/ConsoleInputReader?style=for-the-badge)](https://img.shields.io/nuget/vpre/ConsoleInputReader)

### TestConsoleApp - InputReader Usage Sample

This document provides an overview and usage samples of the `InputReader` library, which is designed to simplify reading various types of inputs in console applications.

## Features

The `InputReader` library offers a range of features to make input handling more straightforward and efficient. These features include:

- **ReadString**: Reads a string input from the console.
- **ReadChar**: Reads a character input from the console.
- **ReadInt**: Reads an integer input and validates it.
- **ReadDateOnly**: Parses a date input with a specific format from the console.
- **ReadTimeOnly**: Parses a time input with a specific format from the console.
- **ReadYesNo**: Reads yes or no input from the console.
- **ReadPassword**: Reads a password input from the console.
- **ReadKey**: Reads a key input from the console.

This readers also have extension methods to read until a valid input is provided. The validation can be customized by passing a predicate to the extension method as well as its type. For instance, you can read the input until it's a valid integer.

## Installation

To use the `InputReader` class library in your project, you need to install it via NuGet Package Manager. You can do this by running the following command in your Package Manager Console:

```bash
Install-Package InputReader ConsoleInputReader
```


### A few examples of how to use the InputReader library:

```csharp

// Read a string input until it's a valid email address
var validEmailResult = Input.String("Enter your email address: ").ReadUntilValidEmail();

// Read an integer input with allowed values. If the input is 1 or 2, it will be valid result, otherwise not valid.
var oneOrTwoResult = Input.Int("Do you agree? ").WithAllowedValues(1, 2).Read();


// Read a char from console and validate it if it's a allowed char
var yesNoResult = Input
                    .YesNo()
                    .WithMessage("Are you sure? ")
                    .WithErrorMessage("Please enter either 'y' or 'n' ")
                    .WithAllowedValues(["y", "n"], caseInsensitive: true)
                    .Read();

// Read an integer input until it's a valid one digit number
var oneDigitIntegerResult = Input
                            .Int("Input a number that is one digit only")
                            .WithPreValidator(input => input?.Length == 1)
                            //.WithAllowedValues(from: 0, to: 9) // In Range
                            .ReadUntilValid();

// Read a date input with a provided format with a specific format
var formattedDate = Input.DateOnly("Enter Date [dd.MM.yyyy]: ")
                     .WithDateOnlyValueConverter(format: "dd.MM.yyyy")
                     .Read();

// Read a date input with a provided format until it's in the specified range
var dateInput = Input
                    .DateOnly("Enter Date [dd.MM.yyyy]: ", format: "dd.MM.yyyy")
                    .ReadUntilInRange("01.01.2021", "31.12.2021", "dd.MM.yyyy");


// Read a time input with a provided format until it's in the specified range
var timeResult_1 = Input
                    .TimeOnly("Enter a time (HH:mm:ss): ", "HH:mm:ss")
                    .ReadUntilInRange("10:00", "18:00", "HH:mm");

// Read a time input with a provided format until it's in the specified range
var timeResult_2_ = Input
                    .TimeOnly("Enter a time (HH:mm:ss): ", "HH:mm:ss")
                    .ReadUntilInRange(CustomTimeOnly.From(hour: 10), CustomTimeOnly.From(hour: 18));

var passWordResult = Input
                        .Password("Enter your password: ", Constants.Chars.NoChar) // No char will be shown
                      //.PassWord("Enter your password: ", Constants.Chars.Asterisk) // Asterisk will be shown")  
                        .Read();


```

All the readers, return a result object that's derived from `InputValue` class. This object contains the input value and a boolean property `IsValid` that indicates if the input is valid or not.
ToString() function is overridden to return the input value as a string. Also, implicit conversion operators are defined to convert the `InputValue` object to the input value type and vice versa.

```csharp
public record InputValue<T>(T Value)
{
    public T Value { get; protected set; } = Value;

    public bool IsValid { get; set; }

    protected internal virtual string DefaultErrorMessage => "Invalid value. Please try again.";

    public sealed override string ToString() => Value?.ToString();

    public static implicit operator T(InputValue<T> wrapper)
    {
        return wrapper.Value;
    }

    public static implicit operator InputValue<T>(T value)
    {
        return new InputValue<T>(value);
    }
}
```

