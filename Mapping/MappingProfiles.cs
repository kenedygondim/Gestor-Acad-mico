using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace ASPDotnetFC.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Curso, CursoDto>().ReverseMap();
            CreateMap<Nota, NotaDto>().ReverseMap();
            CreateMap<Aluno, AlunoDto>().ReverseMap();
            CreateMap<Disciplina, DisciplinaDto>().ReverseMap();
            CreateMap<Professor,ProfessorDto >().ReverseMap();
        }
    }
}
