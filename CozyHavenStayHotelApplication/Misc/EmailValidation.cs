using System.ComponentModel.DataAnnotations;

namespace CozyHavenStayHotelApplication.Misc
{
    public class EmailValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
                return new ValidationResult("Email is required");

            string email = value.ToString()!;
            if (!new EmailAddressAttribute().IsValid(email))
                return new ValidationResult("Invalid email format");

            return ValidationResult.Success;
        }
    }
}