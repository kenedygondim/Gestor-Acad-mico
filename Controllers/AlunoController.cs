using AutoMapper;
using Gestor_Acadêmico.Context;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController(
        GestorAcademicoContext context,
        IAlunoRepository alunoRepository,
        IAlunoDisciplinaRepository alunoDisciplinaRepository,
        IDisciplinaRepository disciplinaRepository,
        INotaRepository notaRepository,
        IMapper mapper
        ) : ControllerBase 
    {
        private readonly GestorAcademicoContext _context = context;
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly IAlunoDisciplinaRepository _alunoDisciplinaRepository = alunoDisciplinaRepository;
        private readonly IDisciplinaRepository _disciplinaRepository = disciplinaRepository;
        private readonly INotaRepository _notaRepository = notaRepository;
        private readonly IMapper _mapper = mapper;  

        static readonly string[] status = ["Matriculado", "Trancado", "Formado", "Desistente", "Afastado"];
        static readonly string[] generos = [ "Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero", "Transgênero" ];
        static readonly string patternCpf = @"(\d{3}\.){2}\d{3}-\d{2}";
        static readonly string patternEmail = @"([a-z0-9\.\-_]{2,})@([a-z0-9]{2,})(\.[a-z]{2,})?(\.[a-z]{2,})?(\.[a-z]{2,})";
        static readonly string patternNumeroDeTelefone = @"(\d{2})\s(\d{4,5}-\d{4})";

        private static string ObterSemestreAtual()
        {
            int anoAtual = DateTime.Now.Year;
            int mesAtual = DateTime.Now.Month;
            string semestreAtual = (mesAtual <= 6) ? "1" : "2";
            return $"{anoAtual}.{semestreAtual}";
        }

        private static string GerarMatriculaAleatoria(IEnumerable<string> matriculasEmUso)
        {
            Random random = new Random();
            string matriculaAleatoria;

            do
                matriculaAleatoria = $"SP{random.Next(1000000, 9999999)}";
            while (matriculasEmUso.Contains(matriculaAleatoria));

            return matriculaAleatoria;
        }

        private static string ObterNomeCompleto(string primeiroNome, string sobrenome) => $"{primeiroNome} {sobrenome}";
        private static bool ValidarCpf(string cpf) => Regex.IsMatch(cpf, patternCpf);
        private static bool ValidarEmail(string email) => Regex.IsMatch(email, patternEmail);
        private static bool ValidarNumeroDeTelefone(string? numeroDeTelefone) => Regex.IsMatch(numeroDeTelefone, patternNumeroDeTelefone);
        private static bool ValidarGenero(string genero) => generos.Contains(genero);
        private static bool ValidarStatus(string status) => status.Contains(status);

///////////////////////////////////////////////////////Controllers//////////////////////////////////////////////////////////////
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AlunoDto>))]
        public async Task<IActionResult> ObterAlunos()
        {
            try
            {
                var alunos = await _alunoRepository.ObterTodosOsAlunos();

                var alunosDto = _mapper.Map<List<AlunoDto>>(alunos);

                return Ok(alunosDto);
            }
            catch (SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (Exception ex)
           {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpGet("{alunoId}/id")]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> ObterAlunoPeloId([FromRoute] int alunoId)
        {
            try
            {
                var aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);

                if (aluno == null)
                    return NotFound("Aluno não encontrado");

                var alunoDto = _mapper.Map<AlunoDto>(aluno);

                return Ok(alunoDto);
            }
            catch (SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpGet("{nomeDoAluno}")]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> ObterAlunoPeloNome([FromRoute] string nomeDoAluno)
        {
            try
            {
                var aluno = await _alunoRepository.ObterAlunoPeloNome(nomeDoAluno);

                if (aluno == null)
                    return NotFound("Aluno não encontrado");

                var alunoDto = _mapper.Map<List<AlunoDto>>(aluno);

                return Ok(alunoDto);
            }
            catch(SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch(Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpGet("{alunoId}/disciplinas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DisciplinaDto>))]
        public async Task<IActionResult> ObterDisciplinasDoAluno([FromRoute] int alunoId)
        {
            try
            {
                var disciplinas = await _alunoDisciplinaRepository.ObterDisciplinasDoAluno(alunoId);

                if (disciplinas == null)
                    return NotFound("Não há disciplinas associadas a esse aluno.");

                var disciplinaDto = _mapper.Map<List<DisciplinaDto>>(disciplinas);
                return Ok(disciplinaDto);
            }
            catch(SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch(Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpGet("{alunoId}/notas")]
        [ProducesResponseType(200, Type = typeof(NotaDto))]
        public async Task<IActionResult> ObterNotasDoAluno([FromRoute] int alunoId)
        {
            try
            {
                var notas = await _alunoRepository.ObterNotasDoAluno(alunoId);

                if (notas == null)
                    return NotFound("Notas não encontradas");

                var notasDto = _mapper.Map<List<NotaDto>>(notas);

                return Ok(notasDto);
            }
            catch (SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }


        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> CriarAluno([FromBody] Aluno aluno)
        {
            if (aluno == null || !ModelState.IsValid)
                return BadRequest("Insira os dados corretamente!");

            if(!ValidarGenero(aluno.Genero))
                return BadRequest($"Insira um genêro válido: {string.Join(", ", generos)}.");

            if(!ValidarStatus(aluno.StatusDoAluno))
                return BadRequest($"Insira uma situação válida: {string.Join(", ", status)}.");

            if (!ValidarCpf(aluno.Cpf))
                return BadRequest("CPF inválido! Padrão desejado: XXX.XXX.XXX-XX");

            if(!ValidarEmail(aluno.EnderecoDeEmail))
                return BadRequest("Endereço de e-mail inválido!");

            if(!ValidarNumeroDeTelefone(aluno.NumeroDeTelefone))
                return BadRequest("Número de telefone inválido! Padrão desejado: XX XXXXX-XXXX");

            using var dbContextTransaction = _context.Database.BeginTransaction();

            try
            {
                var matriculasEmUso = await _alunoRepository.ObterTodosOsNumerosDeMatricula();


                aluno.Matricula = GerarMatriculaAleatoria(matriculasEmUso);
                aluno.PeriodoDeIngresso = ObterSemestreAtual();
                aluno.NomeCompleto = ObterNomeCompleto(aluno.PrimeiroNome, aluno.Sobrenome);
                aluno.IRA = 0;

                await _alunoRepository.CriarAluno(aluno);
                
                Aluno alunoCriado = await _alunoRepository.ObterAlunoPelaMatricula(aluno.Matricula);

                IEnumerable<Disciplina> disciplinasDoCurso = await _disciplinaRepository.ObterDisciplinasDoCurso((int) alunoCriado.CursoId);
                List<Disciplina> disciplinasDoPrimeiroSemestre = disciplinasDoCurso.Where(dis => dis.SemestreDeReferencia == 1).ToList();

                List<AlunoDisciplina> listaDeDisciplinasDoAluno = [];
                List<Nota> listaDeNotasDoAluno = [];
                
                if (!disciplinasDoPrimeiroSemestre.Any())
                    return BadRequest("Não foi possível obter as disciplinas do primeiro semestre do curso.");

                foreach (var disciplina in disciplinasDoPrimeiroSemestre)
                {
                    AlunoDisciplina alunoDisciplina = new()
                    {
                        AlunoId = alunoCriado.Id,
                        DisciplinaId = disciplina.Id
                    };

                    Nota nota = new()
                    {
                        AlunoId = alunoCriado.Id,
                        DisciplinaId = disciplina.Id,
                        PrimeiraAvaliacao = 0,
                        SegundaAvaliacao = 0,
                        Atividades = 0,
                        MediaGeral = 0,
                        FrequenciaDoAluno = 100,
                        NotasFechadas = false,
                        Aprovado = false
                    };


                    listaDeDisciplinasDoAluno.Add(alunoDisciplina);
                    listaDeNotasDoAluno.Add(nota);
                }

                await Task.WhenAll(
                 _alunoDisciplinaRepository.AdicionarAlunoNasDisciplinas(listaDeDisciplinasDoAluno),
                 _notaRepository.CriarNotas(listaDeNotasDoAluno)
                 );

                dbContextTransaction.Commit();

                AlunoDto alunoDto = _mapper.Map<AlunoDto>(aluno);

                return Ok(alunoDto);
            }
            catch (SqlException ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest($"Ocorreu problemas com uma das operações: {ex.Message}");
            }
            catch (Exception ex)
            { 
                dbContextTransaction.Rollback();
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
               
        }


        [HttpPost("{alunoId}/rematricular")]
        [ProducesResponseType(200, Type = typeof(List<Disciplina>))]
        public async Task<IActionResult> RematriculaDoAluno([FromRoute] int alunoId, [FromBody] List<int> disciplinasIds)
        {
            using var dbContextTransaction = _context.Database.BeginTransaction();

            try
            {
                var aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);

                if (aluno == null)
                    return NotFound("Aluno inexistente");

                if (aluno.PeriodoDeIngresso == ObterSemestreAtual())
                    return BadRequest("O aluno não pode ser rematriculado, pois está no primeiro período do curso.");

                if (aluno.StatusDoAluno != "Matriculado")
                    return BadRequest("Somente alunos devidamente matriculados podem realizar o processo de rematrícula.");

                var disciplinasAnteriores = await _alunoDisciplinaRepository.ObterDisciplinasDoAluno(alunoId);
                var notasDoAluno = await _alunoRepository.ObterNotasDoAluno(alunoId);

                List<AlunoDisciplina> disciplinasDesejadas = [];
                List<Nota> gradeDeNotas = [];

                foreach (var disciplinaId in disciplinasIds)
                {
                    var disciplina = await _disciplinaRepository.ObterDisciplinaPeloId(disciplinaId);

                    if (disciplina == null)
                        return NotFound($"Disciplina com id {disciplinaId} é inexistente.");

                    var disciplinaJaCursada = disciplinasAnteriores.Contains(disciplina);
                    var notaDaDisciplinaJaCursada = notasDoAluno.FirstOrDefault(not => not.DisciplinaId == disciplinaId);

                    if (disciplinaJaCursada && (bool)notaDaDisciplinaJaCursada.Aprovado)
                        return BadRequest($"O aluno já foi aprovado na disciplina: {disciplina.NomeDaDisciplina}");

                    var alunoDisciplina = new AlunoDisciplina()
                    {
                        AlunoId = alunoId,
                        DisciplinaId = disciplinaId
                    };

                    var notas = new Nota()
                    {
                        AlunoId = alunoId,
                        DisciplinaId = disciplinaId
                    };


                    disciplinasDesejadas.Add(alunoDisciplina);
                    gradeDeNotas.Add(notas);
                }

                await Task.WhenAll(
                 _alunoDisciplinaRepository.AdicionarAlunoNasDisciplinas(disciplinasDesejadas),
                 _notaRepository.CriarNotas(gradeDeNotas)
                );

                dbContextTransaction.Commit();

                return Ok("Rematrícula realizada com sucesso!");
            }
            catch
            {
                dbContextTransaction.Rollback();
                return BadRequest("Não foi possível efetuar a rematrícula do aluno.");
            }
        }


        [HttpPut("{alunoId}/atualizar")]
        public async Task<IActionResult> AtualizarAluno([FromRoute] int alunoId, [FromBody] Aluno alunoAtualizado)
        {
            try
            {
                var aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);

                if (aluno == null)
                    return NotFound("Aluno inexistente");

                if (!ModelState.IsValid)
                    return BadRequest("Reveja os dados inseridos");

                if (aluno.Id != alunoAtualizado.Id)
                    return BadRequest("Ocorreu um erro na validação dos identificadores.");

                if (alunoAtualizado.Matricula != aluno.Matricula)
                    return BadRequest("O número de matrícula não pode ser alterado.");

                if (alunoAtualizado.Cpf != aluno.Cpf)
                    return BadRequest("O número de CPF não pode ser alterado.");

                if (alunoAtualizado.PrimeiroNome == "" || alunoAtualizado.Sobrenome == "")
                    return BadRequest("Preencha os campos de primeiro nome e sobrenome!");

                if (alunoAtualizado.DataDeNascimento != aluno.DataDeNascimento)
                    return BadRequest("Não é possível alterar a data de nascimento!");

                if (alunoAtualizado.CursoId != aluno.CursoId)
                    return BadRequest("Não é possível alterar o curso!");

                if (alunoAtualizado.PeriodoDeIngresso != aluno.PeriodoDeIngresso)
                    return BadRequest("Não é possível alterar o período de ingresso!");

                if (!ValidarGenero(aluno.Genero))
                    return BadRequest($"Insira um genêro válido: {string.Join(", ", generos)}.");

                if (!ValidarStatus(aluno.StatusDoAluno))
                    return BadRequest($"Insira uma situação válida: {string.Join(", ", status)}.");

                aluno.NomeCompleto = ObterNomeCompleto(aluno.PrimeiroNome, aluno.Sobrenome);

                await _alunoRepository.AtualizarAluno(aluno);

                var alunoDto = _mapper.Map<AlunoDto>(aluno);

                return Ok("Informações alteradas com sucesso!");
            }

            catch (Exception ex) 
            {
                return BadRequest("Não foi possível atualizar as informações do aluno. " + ex.Message);
            }
        }


    }
}
