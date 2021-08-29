namespace ITechArt.Surveys.Foundation
{
    public enum AuthError
    {
        DefaultError,
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
