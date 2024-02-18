using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gestor_Acadêmico.Context;

namespace Gestor_Acadêmico.Repositories {

    public class AlunoRepository(GestorAcademicoContext context) : IAlunoRepository
    {
        private readonly GestorAcademicoContext _context = context;

        public async Task<IEnumerable<Aluno>> ObterTodosOsAlunos() => await _context.Alunos.ToListAsync();

        public async Task<Aluno> ObterAlunoPeloId(int alunoId) => await _context.Alunos.Where(alu => alu.Id == alunoId).Include(x => x.Notas).FirstOrDefaultAsync();

        public async Task<IEnumerable<Aluno>> ObterAlunoPeloNome(string nomeDoAluno) => await _context.Alunos.Where(alu => alu.NomeCompleto.Contains(nomeDoAluno)).OrderBy(alu => alu.NomeCompleto).ToListAsync();

        public async Task<IEnumerable<Nota>> ObterNotasDoAluno(int alunoId) => await _context.Notas.Where(not => not.AlunoId == alunoId).ToListAsync();

        public async Task<Aluno> ObterAlunoPelaMatricula(string matriculaDoAluno) => await _context.Alunos.FirstOrDefaultAsync(alu => alu.Matricula == matriculaDoAluno);

        public async Task<IEnumerable<string>> ObterTodosOsNumerosDeMatricula() => await _context.Alunos.Select(alu => alu.Matricula).ToListAsync();

        public async Task<bool> CriarAluno(Aluno aluno)
        {
            await _context.AddAsync(aluno);
            return await Save();
        }

        public async Task<bool> AtualizarAluno(Aluno aluno)
        {
            _context.Update(aluno);
            return await Save();
        }

        public async Task<bool> ExcluirAluno(Aluno aluno)
        {
            _context.Remove(aluno);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }


    }
}
