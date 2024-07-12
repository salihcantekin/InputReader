using InputReader.Validators.BuiltInValidators;
using InputReader.Validators.BuiltInValidators.Internals;
using System;

namespace InputReader.Validators;

internal class ValidatorBuilder
{
    private static IPreValidator charValidator;
    public static IPreValidator BuildCharInputValidator() => charValidator ??= new CharInputPreValidator();

    private static IPreValidator intValidator;
    public static IPreValidator BuildIntInputPreValidator() => intValidator ??= new IntInputPreValidator();
    private static IPreValidator citizenShipNumberValidator;

    public static IPreValidator BuildCitizenShipNumberValidator() => citizenShipNumberValidator ??= new CitizenShipNumberPreValidator();


    public static IPreValidator SetCustomPreValidator(Func<string, bool> validatorFunc)
    {
        var internalValidator = new InternalPreValidator();
        internalValidator.ValueValidator.ValidatorFunc = validatorFunc;

        return internalValidator;
    }
}