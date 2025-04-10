namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class AdminFilter
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class AdminRequest
    {
        public AdminFilter? Filter { get; set; }
        public Pagination? Pagination { get; set; } = new Pagination();
        public int? SortBy { get; set; } = 1;
    }
}

