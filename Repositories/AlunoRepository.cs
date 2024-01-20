using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gestor_Acadêmico.Context;

namespace Gestor_Acadêmico.Repositories {

    public class AlunoRepository : IAlunoRepository
    {

        private readonly GestorAcademicoContext _context;

        public AlunoRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> CriarAluno(Aluno aluno)
        {
            await _context.AddAsync(aluno);
            return await Save();
        }

        public async Task<Aluno> GetAlunoPeloId(int alunoId)
        {
            return await _context.Alunos.Where(alu => alu.Id == alunoId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Aluno>> GetAlunoPeloNome(string nomeDoAluno)
        {
            return await _context.Alunos.Where(alu => alu.NomeCompleto.Contains(nomeDoAluno)).ToListAsync();
        }

        public async Task<IEnumerable<Nota>> GetNotasDoAluno(int alunoId)
        {
            return await _context.Notas.Where(not => not.AlunoId == alunoId).ToListAsync();   
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> AtualizarAluno(Aluno aluno)
        {
            _context.Update(aluno);
            return await Save();
        }
    }
}
