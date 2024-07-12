using System;
using System.Collections.Generic;
using System.Text;
using InputReader.Validators;

namespace InputReader.InputReaders;
public class CitizenShipNumberOnlyInputReader : BaseInputReader<long?, CitizenShipNumberInputValue>
{
    public CitizenShipNumberOnlyInputReader(string message) : base(message)
    {
        WithPreValidator(ValidatorBuilder.BuildCitizenShipNumberValidator());
    }
    public CitizenShipNumberOnlyInputReader() : this(null)
    {
        
    }
}

