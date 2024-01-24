namespace Gestor_Acadêmico.Dto
{
    public class NotaDto
    {
        public int Id { get; set; }
        public decimal? Frequencia { get; set; }
        public decimal PrimeiraAvaliacao { get; set; }
        public decimal SegundaAvaliacao { get; set; }
        public decimal Atividades { get; set; }
        public required int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public bool NotasFechadas { get; set; }
        public decimal MediaGeral {  get; set; }
        public bool Aprovado { get; set; }
    }
}
