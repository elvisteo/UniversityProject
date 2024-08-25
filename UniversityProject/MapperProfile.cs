using AutoMapper;
using UniversityProject.Models;
using UniversityProject.Models.DTO;

namespace UniversityProject
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<University, UniversityDTO>();
            CreateMap<UniversityDTO, University>();
        }
    }
}
