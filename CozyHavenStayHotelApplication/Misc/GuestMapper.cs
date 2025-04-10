using AutoMapper;
using CozyHavenStayHotelApplication.Models;
using CozyHavenStayHotelApplication.Models.DTOs;

namespace CozyHavenStayHotelApplication.Misc
{
    public class GuestMapper : Profile
    {
        public GuestMapper()
        {
            CreateMap<Guest, GetGuestResponse>();
        }
    }
}
