using CozyHavenStayHotelApplication.Misc;
using System.ComponentModel.DataAnnotations;

namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class CreateAdminRequest
    {
        public string FullName { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailValidation]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}

