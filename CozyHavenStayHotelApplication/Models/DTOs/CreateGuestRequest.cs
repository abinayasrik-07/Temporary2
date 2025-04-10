using System.ComponentModel.DataAnnotations;

namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class CreateGuestRequest
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
    }
}
