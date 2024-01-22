using Gestor_Acadêmico.Context;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gestor_Acadêmico.Repositories
{
    public class AlunoCursoRepository(GestorAcademicoContext context) : IAlunoCursoRepository
    {
        private readonly GestorAcademicoContext _context = context;

        public async Task<bool> AdicionarVariosAlunosAoCurso(AlunoCurso alunoCurso)
        {

            await _context.AddAsync(alunoCurso);
            return await Save();
        }

        public async Task<AlunoCurso> ObterAlunoCursoPeloId(int alunoId, int cursoId)
        {
            return await _context.AlunoCursos.FirstOrDefaultAsync(alu => alu.AlunoId == alunoId && alu.CursoId == cursoId);
        }

        public async Task<IEnumerable<AlunoCurso>> ObterTodosOsAlunosCursos()
        {
            return await _context.AlunoCursos.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
