using System.ComponentModel.DataAnnotations;

namespace CozyHavenStayHotelApplication.Misc
{
    public class NameValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult("Full name is required");

            string strValue = value.ToString()!;
            foreach (char c in strValue)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    return new ValidationResult("Full name must contain only letters and spaces");
            }
            return ValidationResult.Success;
        }
    }
}
