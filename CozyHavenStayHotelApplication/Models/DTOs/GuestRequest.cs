namespace CozyHavenStayHotelApplication.Models.DTOs
{
    public class Range
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
    public class GuestFilter
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public Range? Age { get; set; }
        public string? Nationality { get; set; }
    }
    public class Pagination
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
    public class GuestRequest
    {
        public GuestFilter? Filter { get; set; }
        public Pagination? Pagination { get; set; } = new Pagination();
        public int? SortBy { get; set; } = 1;
        
    }
}
