using CozyHavenStayHotelApplication.Models.DTOs;

namespace CozyHavenStayHotelApplication.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<GetAdminResponse>> GetAllAdmins();
        Task<GetAdminResponse> GetAdminById(int id);
        Task<GetAdminResponse> UpdateAdmin(UpdateAdminRequest request);
        Task<bool> DeleteAdmin(int id);
        Task<AdminLoginResponse> RegisterAdmin(CreateAdminRequest request);
        Task<IEnumerable<GetAdminResponse>> GetAdminsByFilter(AdminRequest request);
    }
}
