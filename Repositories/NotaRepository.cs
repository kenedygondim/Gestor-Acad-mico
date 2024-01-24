using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Context;

namespace Gestor_Acadêmico.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly GestorAcademicoContext _context; 

        public NotaRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> CriarNota(Nota nota)
        {
            Nota notaFix = new()
            {
                Frequencia = nota.Frequencia,
                PrimeiraAvaliacao = nota.PrimeiraAvaliacao,
                SegundaAvaliacao = nota.SegundaAvaliacao,
                Atividades = nota.Atividades,
                NotasFechadas = nota.NotasFechadas,
                MediaGeral = (nota.PrimeiraAvaliacao + nota.SegundaAvaliacao + nota.Atividades) / 3,
                AlunoId = nota.AlunoId,
                DisciplinaId = nota.DisciplinaId,
                Aprovado = ((nota.PrimeiraAvaliacao + nota.SegundaAvaliacao + nota.Atividades) / 3) > 6 && nota.Frequencia > 75 && nota.NotasFechadas == true
            }; 

            await _context.AddAsync(notaFix);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> AtualizarNota(Nota nota)
        {
            _context.Update(nota);
            return await Save();
        }
    }
}
