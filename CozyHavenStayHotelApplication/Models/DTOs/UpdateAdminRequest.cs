namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class UpdateAdminRequest
    {
        public int AdminId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
