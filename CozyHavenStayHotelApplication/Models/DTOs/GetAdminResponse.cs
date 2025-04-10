namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class GetAdminResponse
    {
        public int AdminId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
