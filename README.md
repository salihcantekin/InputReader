
# InputReader

![NuGet Downloads](https://img.shields.io/nuget/dt/ConsoleInputReader?style=for-the-badge)
![NuGet Version](https://img.shields.io/nuget/v/ConsoleInputReader?style=for-the-badge)
![NuGet Pre-release Version](https://img.shields.io/nuget/vpre/ConsoleInputReader?style=for-the-badge)

## Overview

`InputReader` is a C# library designed to simplify and enhance the process of reading user input in console applications. It supports reading and validating various types of input, including strings, integers, dates, times, and more, with an intuitive API.

## Features

- **ReadString**: Reads a string input.
- **ReadChar**: Reads a character input.
- **ReadInt**: Reads an integer input with validation.
- **ReadDateOnly**: Parses and reads a date input in a specific format.
- **ReadTimeOnly**: Parses and reads a time input in a specific format.
- **ReadYesNo**: Reads a yes or no input.
- **ReadPassword**: Reads a password input securely.
- **ReadKey**: Captures a key press input.

Each method includes options for validating the input and can be customized using predicates to ensure the input meets specific criteria.

## Installation

You can install the `InputReader` package via NuGet by using the following command:

```bash
Install-Package ConsoleInputReader
```

## Usage Examples

### Basic Examples

```csharp
// Read a string input until it's a valid email address
var validEmail = Input.String("Enter your email: ").ReadUntilValidEmail();

// Read an integer input with allowed values. If the input is 1 or 2, it will be a valid result, otherwise invalid.
var oneOrTwo = Input.Int("Do you agree? ").WithAllowedValues(1, 2).Read();
```

### Advanced Examples

```csharp
// Read a yes/no input with custom messages and allowed values
var yesNo = Input.YesNo()
    .WithMessage("Are you sure? ")
    .WithErrorMessage("Please enter 'y' or 'n'")
    .WithAllowedValues(new[] { "y", "n" }, caseInsensitive: true)
    .Read();

// Read an integer input until it's a valid one-digit number
var oneDigitInteger = Input.Int("Input a number that is one digit only")
    .WithPreValidator(input => input?.Length == 1)
    //.WithAllowedValues(from: 0, to: 9) // In Range
    .ReadUntilValid();

// Read a date input in the format "dd.MM.yyyy"
var formattedDate = Input.DateOnly("Enter Date [dd.MM.yyyy]: ")
    .WithDateOnlyValueConverter("dd.MM.yyyy")
    .Read();

// Read a date input until it's within a specified range
var dateInput = Input.DateOnly("Enter Date [dd.MM.yyyy]: ", format: "dd.MM.yyyy")
    .ReadUntilInRange("01.01.2021", "31.12.2021", "dd.MM.yyyy");

// Read a time input in the format "HH:mm:ss" until it's within a specified range
var timeResult_1 = Input.TimeOnly("Enter a time (HH:mm:ss): ", "HH:mm:ss")
    .ReadUntilInRange("10:00", "18:00", "HH:mm");

// Alternatively, using custom time objects for range validation
var timeResult_2 = Input.TimeOnly("Enter a time (HH:mm:ss): ", "HH:mm:ss")
    .ReadUntilInRange(CustomTimeOnly.From(hour: 10), CustomTimeOnly.From(hour: 18));

// Read a password input securely without displaying characters
var passWordResult = Input.Password("Enter your password: ", Constants.Chars.NoChar).Read();
// Optional: Show asterisks while typing
// var passWordResult = Input.Password("Enter your password: ", Constants.Chars.Asterisk).Read();

// Capture a key press and validate if it's the 'R' key
var keyResult = Input.Key("Press any key: ")
    //.ReadUntil(input => input.Is(ConsoleKey.R))
    //.ReadUntil(input => input.Is(ConsoleKey.R) && input.Modifiers.HasFlag(ConsoleModifiers.Control))
    .ReadUntil(input => input.IsKeyChar('R'));
```

## InputValue Class

All methods return an `InputValue` object that contains the input value and a boolean `IsValid` indicating whether the input is valid. The `InputValue` class supports implicit conversion to and from the input type, providing flexibility in handling inputs.

```csharp
public record InputValue<T>(T Value)
{
    public T Value { get; protected set; } = Value;
    public bool IsValid { get; set; }
    protected internal virtual string DefaultErrorMessage => "Invalid value. Please try again.";
    public sealed override string ToString() => Value?.ToString();

    public static implicit operator T(InputValue<T> wrapper) => wrapper.Value;
    public static implicit operator InputValue<T>(T value) => new InputValue<T>(value);
}
```
