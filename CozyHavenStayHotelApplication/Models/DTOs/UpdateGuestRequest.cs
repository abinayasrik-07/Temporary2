using System.ComponentModel.DataAnnotations;

namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class UpdateGuestRequest
    {
        public int GuestId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

}
