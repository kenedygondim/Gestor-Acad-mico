using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Context;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Repositories
{
    public class DisciplinaRepository(GestorAcademicoContext context) : IDisciplinaRepository
    {
        private readonly GestorAcademicoContext _context = context;

        public async Task<IEnumerable<Disciplina>> ObterDisciplinas() => await _context.Disciplinas.ToListAsync();

        public async Task<Disciplina> ObterDisciplinaPeloId(int disciplinaId) => await _context.Disciplinas.FirstOrDefaultAsync(dis => dis.Id == disciplinaId);

        public async Task<IEnumerable<Disciplina>> ObterDisciplinaPeloNome(string nomeDaDisciplina) => await _context.Disciplinas.Where(dis => dis.NomeDaDisciplina.Contains(nomeDaDisciplina)).ToListAsync();

        public async Task<IEnumerable<Disciplina>> ObterDisciplinasDoCurso(int cursoId) => await _context.Disciplinas.Where(dis => dis.CursoId == cursoId).ToListAsync();

        public async Task<IEnumerable<Disciplina>> ObterDisciplinasDoProfessor(int professorId) => await _context.Disciplinas.Where(dis => dis.ProfessorId == professorId).ToListAsync();

        public async Task<bool> CriarDisciplina(Disciplina disciplina)
        {
            await _context.AddAsync(disciplina);
            return await Save();
        }

        public async Task<bool> AtualizarDisciplina(Disciplina disciplina)
        {
            _context.Update(disciplina);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
