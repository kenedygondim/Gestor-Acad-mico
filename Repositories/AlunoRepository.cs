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
            int anoAtual = DateTime.Now.Year;
            int mesAtual = DateTime.Now.Month;
            int semestreAtual = (mesAtual <= 6) ? 1 : 2;

            Aluno alunoCriado = new() 
            {
                StatusDoAluno = aluno.StatusDoAluno,
                Matricula = aluno.Matricula,
                CursoId = aluno.CursoId,
                PeriodoDeIngresso = $"{anoAtual}.{semestreAtual}",  
                PrimeiroNome = aluno.PrimeiroNome,
                Sobrenome = aluno.Sobrenome,
                NomeCompleto = $"{aluno.PrimeiroNome} {aluno.Sobrenome}",
                Cpf = aluno.Cpf,
                Genero = aluno.Genero,
                NumeroDeTelefone = aluno.NumeroDeTelefone,
                EnderecoDeEmail = aluno.EnderecoDeEmail,
                DataDeNascimento = aluno.DataDeNascimento,  
                Endereco = aluno.Endereco,  
            };

            await _context.AddAsync(alunoCriado);
            return await Save();
        }

        public async Task<Aluno> ObterAlunoPeloId(int alunoId)
        {
            return await _context.Alunos.Where(alu => alu.Id == alunoId).Include(alu => alu.Notas).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Aluno>> ObterAlunoPeloNome(string nomeDoAluno)
        {
            return await _context.Alunos.Where(alu => alu.NomeCompleto.Contains(nomeDoAluno)).ToListAsync();
        }

        public async Task<IEnumerable<Nota>> ObterNotasDoAluno(int alunoId)
        {
            return await _context.Notas.Where(not => not.AlunoId == alunoId).ToListAsync();   
        }

        public async Task<IEnumerable<Aluno>> ObterAlunos()
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
            var alunoFix = await ObterAlunoPeloId(aluno.Id);

            if (alunoFix.Notas.Any())
            {
                aluno.IRA = alunoFix.Notas.Sum(not => not.MediaGeral) / alunoFix.Notas.Count();
            }
            else
            {
                aluno.IRA = 0;
            }

            _context.Update(aluno);
            return await Save();
        }

        public async Task<Aluno> ObterAlunoPelaMatricula(string matriculaDoAluno)
        {
            return await _context.Alunos.Where(alu => alu.Matricula == matriculaDoAluno).FirstOrDefaultAsync();
        }
    }
}
