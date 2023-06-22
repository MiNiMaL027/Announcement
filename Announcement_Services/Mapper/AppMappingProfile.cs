using Announcement_Domain.CreateModels;
using Announcement_Domain.DtoModels;
using Announcement_Domain.Models;
using AutoMapper;

namespace Task_Service.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() 
        {
            CreateProjection<DtoAnnouncement, Announcement>();

            CreateMap<DtoAnnouncement, Announcement>().ReverseMap();
            CreateMap<CreateAnnouncement, Announcement>().ReverseMap();
        }
    }
}
