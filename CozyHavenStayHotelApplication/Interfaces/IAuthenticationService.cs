using CozyHavenStayHotelApplication.Models.DTOs;

namespace CozyHavenStayHotelApplication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<object> Login(LoginRequest loginRequest);
    }
}
