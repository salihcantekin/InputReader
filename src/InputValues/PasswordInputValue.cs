using System;
using System.Collections.Generic;
using System.Text;

namespace InputReader.InputValues;
public record PasswordInputValue(string Value) : InputValue<string>(Value)
{
}
