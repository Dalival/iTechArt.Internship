using System;
using System.Collections.Generic;
using System.Linq;

namespace ITechArt.Surveys.Foundation.Result
{
    public class RegistrationResult
    {
        public static RegistrationResult Success { get; } = new(null);


        public bool Succeeded { get; }

        public IReadOnlyCollection<RegistrationError> Errors { get; }


        private RegistrationResult(IReadOnlyCollection<RegistrationError> errors)
        {
            if (errors == null || !errors.Any())
            {
                Succeeded = true;
            }
            else
            {
                Succeeded = false;
                Errors = errors;
            }
        }


        public static RegistrationResult Failed(IReadOnlyCollection<RegistrationError> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors), "If result is failed, then collection of errors shouldn't be null.");
            }

            if (!errors.Any())
            {
                throw new ArgumentException("If result is failed, then collection of errors shouldn't be empty.",
                    nameof(errors));
            }

            var result = new RegistrationResult(errors);

            return result;
        }

        public static RegistrationResult Failed(RegistrationError error)
        {
            var errors = new List<RegistrationError> { error };
            var result = new RegistrationResult(errors);

            return result;
        }
    }
}
