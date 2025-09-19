using System.ComponentModel.DataAnnotations;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)  // value means email
            return ValidationResult.Success; // means no errors

        var dbContext = validationContext.GetService<AppDbContext>();
        var email = value.ToString()!.Trim().ToLower();
        bool emailExists = false;
        if (dbContext != null)
        {
            emailExists = dbContext.Users.Any(u => u.Email.ToLower() == email);
        }

        if (emailExists)
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }

        return ValidationResult.Success;
    }
    public override string FormatErrorMessage(string name)
    {
        if (string.IsNullOrEmpty(ErrorMessage))
        {
            return $"{name} is already registered."; 
        }
        return base.FormatErrorMessage(name);
    }
    
}
