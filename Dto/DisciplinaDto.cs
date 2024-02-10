namespace Gestor_Acadêmico.Dto
{
    public record DisciplinaDto
        (
        int Id,
        string NomeDaDisciplina,
        string CodigoDaDisciplina,
        decimal CargaHoraria,
        int AulasPorSemana,
        int SemestreDeReferencia,
        string SituacaoDaDisciplina,
        int? ProfessorId,
        int? CursoId
        );
}
