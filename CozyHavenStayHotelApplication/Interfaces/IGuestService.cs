using CozyHavenStayHotelApplication.Models.DTOs;

namespace CozyHavenStayHotelApplication.Interfaces
{
    public interface IGuestService
    {
        Task<CreateGuestResponse> RegisterGuest(CreateGuestRequest request);
        Task<IEnumerable<GetGuestResponse>> GetAllGuests();
        Task<GetGuestResponse> GetGuestById(int guestId);
        Task<GetGuestResponse> UpdateGuestDetails(UpdateGuestRequest request);
        Task<bool> DeleteGuest(int guestId);
        Task<IEnumerable<GetGuestResponse>> GetGuestsByFilter(GuestRequest request);
    }
}