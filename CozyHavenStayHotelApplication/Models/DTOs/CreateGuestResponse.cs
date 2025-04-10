namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class CreateGuestResponse
    {
        public int GuestId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
