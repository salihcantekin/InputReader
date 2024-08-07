using InputReader;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Extensions;


// Read a string input until it's a valid email address
var validEmailResult = Input
                         .String("Enter your email address: ")
                         .ReadUntilValidEmail();

// Read an integer input with allowed values. If the input is 1 or 2, it will be valid result, otherwise not valid.
var oneOrTwoResult = Input
                       .Int("Do you agree? ")
                       .WithAllowedValues(1, 2)
                       .Read();


// Read a char from console and validate it if it's a allowed char
var yesNoResult = Input
                    .YesNo()
                    .WithMessage("Are you sure? ")
                    .WithErrorMessage("Please enter either 'y' or 'n' ")
                    //.WithAllowedValues(['y', 'y'], caseInsensitive: true) // for YesNo, WithAllowedValues is not necessary, it's already set
                    .Read();

// Read an integer input until it's a valid one digit number
var oneDigitIntegerResult = Input
                              .Int("Input a number that is one digit only")
                              .With(builder =>
                              {
                                  builder.WithPreValidator(input => input?.Length == 1);
                              })
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
                       .Read();

var keyResult = Input
                    .Key("Press any key: ")
                    //.ReadUntil(input => input.Is(ConsoleKey.R))
                    //.ReadUntil(input => 
                    //{
                    //    return input.Value.Modifiers.HasFlag(ConsoleModifiers.Control) && input.Is(ConsoleKey.R);
                    //})
                    .ReadUntil(input => input.IsKeyChar('R'));