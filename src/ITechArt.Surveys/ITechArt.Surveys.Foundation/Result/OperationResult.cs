using System;
using System.Collections.Generic;
using System.Linq;

namespace ITechArt.Surveys.Foundation.Result
{
    public readonly struct OperationResult<TError>
    {
        public static OperationResult<TError> Success { get; } = new(null);


        public bool Succeeded { get; }

        public IReadOnlyCollection<TError> Errors { get; }


        private OperationResult(IReadOnlyCollection<TError> errors)
        {
            if (errors == null || !errors.Any())
            {
                Succeeded = true;
                Errors = null;
            }
            else
            {
                Succeeded = false;
                Errors = errors;
            }
        }


        public static OperationResult<TError> Failed(IReadOnlyCollection<TError> errors)
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

            var result = new OperationResult<TError>(errors);

            return result;
        }

        public static OperationResult<TError> Failed(TError error)
        {
            var errors = new List<TError> { error };
            var result = new OperationResult<TError>(errors);

            return result;
        }
    }
}
