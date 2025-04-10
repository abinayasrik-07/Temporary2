using AutoMapper;
using CozyHavenStayHotelApplication.Models;
using CozyHavenStayHotelApplication.Models.DTOs;

namespace CozyHavenStayHotelApplication.Misc
{
    public class AdminMapper : Profile
    {
        public AdminMapper()
        {
            CreateMap<Admin, GetAdminResponse>();
        }
    }
}
