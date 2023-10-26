using AutoMapper;
using DataAccessLayer.Entity;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

       
        public static Student UpdateStudent(StudentModel studentModel, Student student)
        {
            var updatestudent = _mapper.Map(studentModel, student);
            return updatestudent;
        }

        public static CourseModel ToCourseModel(this Course course)
        {
            return _mapper.Map<CourseModel>(course);
        }

        public static Course ToCourse(this CourseModel courseModel)
        {
            return _mapper.Map<Course>(courseModel);
        }

        public static List<CourseModel> ToCourseModelList(IEnumerable<Course> Course)
        {
            return _mapper.Map<List<CourseModel>>(Course);
        }

        public static Course UpdateCourse(CourseModel courseModel, Course course)
        {
            var updatecourse = _mapper.Map(courseModel, course);
            return updatecourse;
        }


        public static EnrollmentModel ToEnrollmentModel(this Enrollment enrollment)
        {
            return _mapper.Map<EnrollmentModel>(enrollment);
        }

        public static Enrollment ToEnrollment(this EnrollmentModel enrollmentModel)
        {
            return _mapper.Map<Enrollment>(enrollmentModel);
        }

        public static List<EnrollmentModel> ToEnrollmentModelList(IEnumerable<Enrollment> enrollment)
        {
            return _mapper.Map<List<EnrollmentModel>>(enrollment);
        }

        public static Enrollment UpdateEnrollment(EnrollmentModel enrollmentModel,Enrollment enrollment)
        {
            var updateenrollment = _mapper.Map(enrollmentModel, enrollment);
            return updateenrollment;
        }


    }
}
