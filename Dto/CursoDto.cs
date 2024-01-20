using Gestor_Acadêmico.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class CursoDto
    {
        public int Id { get; set; }
        public required string NomeDoCurso { get; set; }
        public required int QuantidadeDeSemestres { get; set; }
        public int VagasNoPrimeiroSemestre { get; set; }
        public int VagasNoSegundoSemestre { get; set; }
        public required int DuracaoDoSemestreEmSemanas { get; set; }
        public required string Modalidade { get; set; }
        public required string Turno { get; set; }
        public required string CategoriaDoCurso { get; set; }
        public required decimal CargaHoraria { get; set; }
    }
}

