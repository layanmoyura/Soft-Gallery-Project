using AutoMapper;
using PresentationLayer.Models;
using DataAccessLayer.Entity;


namespace PresentationLayer.helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentModel>().ReverseMap();
            CreateMap<Course, CourseModel>().ReverseMap();
            CreateMap<Enrollment, EnrollmentModel>().ReverseMap();

        }
    }
}
