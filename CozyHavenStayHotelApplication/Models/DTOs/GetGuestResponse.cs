namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class GetGuestResponse
    {
        public int GuestId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
