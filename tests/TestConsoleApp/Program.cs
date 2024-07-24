﻿using InputReader;
using InputReader.InputReaders.Extensions;


//var dateInput = Input.DateOnly("Enter Date [dd.MM.yyyy]: ", format: DateFormat)
//                    .ReadUntilInRange(fromDate: "01.01.2021",
//                                      toDate: "31.12.2021",
//                                      "dd.MM.yyyy");

var passResult = Input.Password("Enter your password: ", Constants.Chars.NoChar).Read();

Console.WriteLine("IsValid: " + passResult.IsValid);
Console.WriteLine("Value: " + passResult.Value);

var ynResult = Input.Int("\nNumber: ")
                .WithErrorMessage("Please enter value greater than 100")
                //.WithIteration((value, printProcessor) =>
                //{
                //    if (value.IsLessThan(100))
                //    {
                //        printProcessor.Print("Please enter value greater than 100");
                //    }
                //})
                .With(opt =>
                {
                    //opt.WithPreValidator(i =>
                    //{
                    //    var number = int.Parse(i);
                    //    return number > 100;
                    //});

                    opt.WithCustomConverter(message =>
                    {
                        return int.Parse(message) * 2;
                    });
                })
                //.WithErrorMessage("Invalid input. Please enter 'y' or 'n'.")
                .ReadUntilValid();

Console.WriteLine(ynResult);
Console.WriteLine("IsValid: " + ynResult.IsValid);
Console.WriteLine("Value: " + ynResult.Value);

return;
var iteractionTest = Input
                    .Int("Enter a number: ")
                    .WithIteration((input, printer) =>
                    {
                        if (!input.IsValid)
                            printer.PrintLine("Invalid input. Please try again.");
                        else
                            printer.PrintLine("You entered: " + input.Value);
                    })
                    .ReadUntilValid();

var passwordResult = Input
                    .Password("Enter your password: ")
                    .ReadUntilValid();

var oneDigitInteger = Input
                .Int("Input a number that is one digit only")
                .With(builder =>
                {
                    builder.WithPreValidator(input => input?.Length == 1);
                })
                .ReadUntilValid();

//var intResult = Input
//                .Char("Enter a char: ")
//                .ReadUntil(i =>
//                {
//                    return i > 'a' && i < 'e';
//                });

//var yesNo

//Console.WriteLine("IsValid: " + intResult.IsValid);
//Console.WriteLine("Value: " + intResult.Value);

var intResult = Input
                    .DateOnly()
                    //.ReadUntilValid()
                    .ReadUntilValid();
//.ReadUntil(number =>
//{
//    if (number.IsZero())
//        return true;

//    return false;
//});




//if (yesNoResult.IsYes())
//{
//    // doSomething
//}



//int myNumber = oneDigitInteger;

//if (oneDigitInteger.IsZero())
//{

//    // doSomething
//}

//var input = Input.String("Your Email Address: ")
//                    .ReadUntilValidEmail();
//string str = input;


//var result = Input.Int("Do you agree? ")
//                    .WithValueConverter((val) =>
//                    {
//                        return int.Parse(val);
//                    })
//                    //.WithAllowedValues(1, 2)
//                    .Read();

////.WithValueConverter(new CustomIntConverter())
//// .WithValueConverter((val) =>
//// {
////     var success = int.TryParse(val, out var value);
////
////     if (success)
////         value = value * 2;
////
////     return new IntInputValue(value);
//// })


////Console.WriteLine("IsValid: " + result.IsValid);
////Console.WriteLine("Value: {0}", result.Value);

//////return;
////var charInputResult = Input
////                        .Char()
////                        .WithMessage("Char input message ")
////                        .Read();


////Console.WriteLine("IsValid: " + charInputResult.IsValid);





////Console.WriteLine("IsYes: " + yesNoResult.IsYes());
////Console.WriteLine("IsValid: " + yesNoResult.IsValid);

////var oneDigitInteger = Input
////                .Int("Input a number that is one digit only")
////                .WithPreValidator(input => input?.Length == 1)
////                .ReadUntilValid();

////Console.WriteLine("IsValid: " + intResult.IsValid);
////Console.WriteLine("Value: " + intResult.Value);

//.WithDateOnlyValueConverter(format: "dd.MM.yyyy")
//.Read();

//Console.WriteLine("IsValid: " + dateInput.IsValid);
//Console.WriteLine("Value: " + dateInput.Value);

//var timeResult = Input
//                    .TimeOnly("Enter a time (HH:mm:ss): ", "HH:mm:ss")
//                    .ReadUntilInRange(CustomTimeOnly.From(hour: 10), 
//                                      CustomTimeOnly.From(hour: 18));
////.ReadUntilInRange("10:00", "18:00", "HH:mm");
////.Read();

//Console.WriteLine("IsValid: " + timeResult.IsValid);
//Console.WriteLine("Value: " + timeResult.Value);
