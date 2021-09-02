namespace ITechArt.Surveys.Foundation
{
    public enum RegistrationError
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
