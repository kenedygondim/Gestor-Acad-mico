using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace ASPDotnetFC.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Grade, GradeDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Teacher,TeacherDto >().ReverseMap();
        }
    }
}
