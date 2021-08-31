namespace ITechArt.Surveys.Foundation
{
    public enum AuthenticationError
    {
        UnknownError,
        InvalidUserName,
        DuplicateUserName,
        InvalidEmail,
        DuplicateEmail,
        PasswordTooShort,
        PasswordRequiresDigit,
        PasswordRequiresLower,
        PasswordRequiresUpper,
        PasswordRequiresUniqueChars,
        PasswordRequiresNonAlphanumeric,
        PasswordConfirmationIncorrect
    }
}
