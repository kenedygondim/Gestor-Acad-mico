namespace Gestor_Acadêmico.Dto
{
    public record NotaDto
        (
           int Id,
           decimal? FrequenciaDoAluno,
           decimal PrimeiraAvaliacao,
           decimal SegundaAvaliacao,
           decimal Atividades,
           int AlunoId,
           int DisciplinaId,
           bool NotasFechadas,
           decimal MediaGeral,
           bool Aprovado
          );
}
