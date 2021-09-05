using System;
using System.Collections.Generic;
using System.Linq;

namespace ITechArt.Surveys.Foundation.Result
{
    public class IdentificationResult<TError>
    {
        public static IdentificationResult<TError> Success { get; } = new(null);


        public bool Succeeded { get; }

        public IReadOnlyCollection<TError> Errors { get; }


        private IdentificationResult(IReadOnlyCollection<TError> errors)
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


        public static IdentificationResult<TError> Failed(IReadOnlyCollection<TError> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors),
                    "If result is failed, then collection of errors shouldn't be null.");
            }

            if (!errors.Any())
            {
                throw new ArgumentException("If result is failed, then collection of errors shouldn't be empty.",
                    nameof(errors));
            }

            var result = new IdentificationResult<TError>(errors);

            return result;
        }

        public static IdentificationResult<TError> Failed(TError error)
        {
            var errors = new List<TError> { error };
            var result = new IdentificationResult<TError>(errors);

            return result;
        }
    }
}
