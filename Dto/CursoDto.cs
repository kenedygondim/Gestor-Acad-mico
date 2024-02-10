namespace Gestor_Acadêmico.Dto
    {
        public record CursoDto
        (
            int Id,
            string NomeDoCurso,
            int QuantidadeDeSemestres,
            int VagasNoPrimeiroSemestre,
            int VagasNoSegundoSemestre,
            int DuracaoDoSemestreEmSemanas,
            string Modalidade,
            string Turno,
            string CategoriaDoCurso,
            decimal CargaHoraria
    );
}

