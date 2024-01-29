using Gestor_Acadêmico.Context;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Repositories
{
    public class AlunoDisciplinaRepository : IAlunoDisciplinaRepository
    {
        private readonly GestorAcademicoContext _context;

        public AlunoDisciplinaRepository(GestorAcademicoContext context)
        {
            _context = context;
        }

        public async Task<bool> AdicionarAlunoNasDisciplinas(List<AlunoDisciplina> alunoDisciplina)
        {
            await _context.AddRangeAsync(alunoDisciplina);
            return await Save();
        }

        public async Task<IEnumerable<Disciplina>> ObterDisciplinasDoAluno(int alunoId)
        {
            return await _context.AlunoDisciplinas
                .Where(ad => ad.AlunoId == alunoId)
                .Select(ad => ad.Disciplina)
                .ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
