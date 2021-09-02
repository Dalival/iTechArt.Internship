using System.Collections.Generic;

namespace ITechArt.Surveys.Foundation
{
    public class RegistrationResult
    {
        public static RegistrationResult Success { get; } = new() { Succeeded = true };


        private List<RegistrationError> _errors = new();


        public bool Succeeded { get; protected set; }

        public IEnumerable<RegistrationError> Errors => _errors;


        public static RegistrationResult Failed(params RegistrationError[] errors)
        {
            var result = new RegistrationResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }

            return result;
        }
    }
}
