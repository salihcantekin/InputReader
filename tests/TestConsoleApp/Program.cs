using InputReader;
using InputReader.InputReaders.Extensions;

//var input = Input.String("Your Email Address: ").ReadValidEmail();

//var result = Input.Int("Do you agree? ").WithAllowedValues(["1", "2"], caseInsensitive: true)

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
//.ReadUntilValid();

//Console.WriteLine("IsValid: " + result.IsValid);
//Console.WriteLine("Value: {0}", result.Value);

////return;
//var charInputResult = Input
//                        .Char()
//                        .WithMessage("Char input message ")
//                        .Read();


//Console.WriteLine("IsValid: " + charInputResult.IsValid);


//var yesNoResult = Input
//    .YesNo()
//    .WithMessage("Are you sure? ")
//    .WithAllowedValues(["y", "n"], true)
//    .Read();

//Console.WriteLine("IsYes: " + yesNoResult.IsYes());
//Console.WriteLine("IsValid: " + yesNoResult.IsValid);

//var intResult = Input
//    .Int()
//    .WithMessage("Input a number: ")
//    .WithPreValidator(input => input?.Length == 1)
//    .Read();

//Console.WriteLine("IsValid: " + intResult.IsValid);
//Console.WriteLine("Value: " + intResult.Value);

var dateInput = Input.DateOnly("Enter Date [dd.MM.yyyy]: ")
                    .WithDateOnlyValueConverter(format: "dd.MM.yyyy")
                    .ReadUntilInRange("01.01.2021", "31.12.2021", "dd.MM.yyyy");
//.Read();

Console.WriteLine("IsValid: " + dateInput.IsValid);
Console.WriteLine("Value: " + dateInput.Value);

var timeResult = Input.TimeOnly("Enter a time (HH:mm:ss): ", "HH:mm:ss")
                    .ReadUntilInRange("10:00", "18:00", "HH:mm");
                    //.Read();

Console.WriteLine("IsValid: " + timeResult.IsValid);
Console.WriteLine("Value: " + timeResult.Value);

