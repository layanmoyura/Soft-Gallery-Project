using AutoMapper;
using ContosoUniversity.Models;
using ContosoUniversity.Entity;


namespace ContosoUniversity.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentModel>().ReverseMap();

        }
    }
}
