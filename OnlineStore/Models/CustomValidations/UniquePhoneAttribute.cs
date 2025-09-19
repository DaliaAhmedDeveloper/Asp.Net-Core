using System.ComponentModel.DataAnnotations;
public class UniquePhoneAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)  // value means phone
            return ValidationResult.Success; // means no errors

        var dbContext = validationContext.GetService<AppDbContext>();
        var phone = value.ToString()!.Trim();

        bool emailExists = false;
        if (dbContext != null)
        {
            emailExists = dbContext.Users.Any(u => u.PhoneNumber.ToLower() == phone);
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
        // لو مفيش رسالة مخصصة، استخدم رسالة افتراضية
        if (string.IsNullOrEmpty(ErrorMessage))
        {
            return $"{name} is already registered.";  // الرسالة الافتراضية لو مفيش رسالة
        }
        return base.FormatErrorMessage(name);
    }
    
}
