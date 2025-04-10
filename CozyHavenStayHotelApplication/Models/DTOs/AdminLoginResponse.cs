namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class AdminLoginResponse
    {
        public int AdminId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}

