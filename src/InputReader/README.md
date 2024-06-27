# ConsoleInputHelper Library

The ConsoleInputHelper library is designed to simplify the process of reading various types of input from the console in .NET applications. It provides a set of methods that make it easy to get input like strings, integers, dates, and more, with built-in validation and error handling.

## Features

- Easy to use methods for reading different types of input from the console.
- Built-in validation for common data types.
- Customizable error messages and retry mechanisms.
- Supports .NET Standard and .NET Core.

## Installation

To use the ConsoleInputHelper library in your project, you can download it directly from the NuGet package manager or use the Package Manager Console:


## Usage

Below are some examples of how to use the ConsoleInputHelper library to read various types of input from the console.

### Reading a String

using InputReader;

string name = ConsoleInput.ReadString("Enter your name: "); 
Console.WriteLine($"Hello, {name}!");



### Reading a Date

using InputReader;

DateTime birthDate = ConsoleInput.ReadDate("Enter your birth date (YYYY-MM-DD): "); 
Console.WriteLine($"Your birth date is {birthDate.ToShortDateString()}.");
