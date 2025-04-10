namespace CozyHavenStayHotelApplication.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(int id, string name);
    }
}
