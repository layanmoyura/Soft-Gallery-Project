using AutoMapper;
using DataAccessLayer.Entity;
using PresentationLayer.Models;
using System.Collections.Generic;

namespace PresentationLayer.helper
{
    public static class MappingFunctions
    {
        private static IMapper _mapper;

        static MappingFunctions()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public static StudentModel ToStudentModel(Student student)
        {
            return _mapper.Map<StudentModel>(student);
        }

        public static Student ToStudent(StudentModel studentModel)
        {
            return _mapper.Map<Student>(studentModel);
        }

        public static List<StudentModel> ToStudentModelList(IEnumerable<Student> students)
        {
            return _mapper.Map<List<StudentModel>>(students);
        }

        public static List<Student> ToStudentList(IEnumerable<StudentModel> studentModels)
        {
            return _mapper.Map<List<Student>>(studentModels);
        }

        public static Student UpdateStudent(StudentModel studentModel, Student student)
        {
            var updatestudent = _mapper.Map(studentModel, student);
            return updatestudent;
        }

    }
}
