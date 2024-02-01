using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController(
        IAlunoRepository alunoRepository,
        IAlunoDisciplinaRepository alunoDisciplinaRepository,
        IDisciplinaRepository disciplinaRepository,
        INotaRepository notaRepository,
        IMapper mapper
        ) : ControllerBase
    {
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly IAlunoDisciplinaRepository _alunoDisciplinaRepository = alunoDisciplinaRepository;
        private readonly IDisciplinaRepository _disciplinaRepository = disciplinaRepository;
        private readonly INotaRepository _notaRepository = notaRepository;
        private readonly IMapper _mapper = mapper;

        readonly string[] status = ["Matriculado", "Trancado", "Formado", "Desistente", "Afastado"];
        readonly string[] generos = [ "Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero", "Transgênero" ];

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
            catch
            {
                return BadRequest("Não foi possível recuperar a lista de alunos");
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
            catch
            {
                return BadRequest("Não foi possível recuperar o aluno solicitado");
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
            catch
            {
                return BadRequest("Não foi possível recuperar as notas do aluno.");
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
            catch
            {
                return BadRequest("Não foi possível recuperar o aluno solicitado");
            }
        }

        [HttpGet("{alunoId}/id/disciplinas")]
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
            catch
            {
                return BadRequest("Não foi possível recuperar as disciplinas do aluno solicitado");
            }
        }

        //Código comentado para maior entendimento.
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> CriarAluno([FromBody] Aluno aluno)
        {
            Random random = new();

            if (aluno == null || !ModelState.IsValid)
                return BadRequest("Insira os dados corretamente!");
            if (!status.Contains(aluno.StatusDoAluno, StringComparer.OrdinalIgnoreCase))
                return BadRequest($"Insira uma situação válida: {string.Join(", ", status)}.");
            if (!generos.Contains(aluno.Genero, StringComparer.OrdinalIgnoreCase))
                return BadRequest($"Insira um genêro válido: {string.Join(", ", generos)}.");

            string matriculaGerada = $"SP{random.Next(1000000, 9999999)}";

            try
            {
                //Verificando se a matrícula gerada já existe no banco de dados.
                var alunoJaExiste = await _alunoRepository.ObterAlunoPelaMatricula(matriculaGerada);

                //Enquanto a matrícula gerada já existir no banco de dados, o método gerará uma nova matrícula.
                while (alunoJaExiste != null)
                {
                    matriculaGerada = $"SP{random.Next(1000000, 9999999)}";
                    alunoJaExiste = await _alunoRepository.ObterAlunoPelaMatricula(matriculaGerada);
                }

                //Quando for encontrado uma matrícula que não exista no banco de dados, a matrícula gerada será atribuída ao aluno.
                aluno.Matricula = matriculaGerada;

                //Criação do aluno
                await _alunoRepository.CriarAluno(aluno);

                //Obtendo o aluno criado
                var alunoCriado = await _alunoRepository.ObterAlunoPelaMatricula(aluno.Matricula);

                //Obtendo as disciplinas do curso do aluno
                var disciplinasDoCurso = await _disciplinaRepository.ObterDisciplinasDoCurso((int) alunoCriado.CursoId);

                //Filtrando as disciplinas do primeiro semestre do curso localmente
                var disciplinasDoPrimeiroSemestre = disciplinasDoCurso.Where(dis => dis.SemestreDeReferencia == 1);

                //Criando uma lista de AlunoDisciplina e uma lista de Nota para o aluno para fins de verificação de erros.
                List<AlunoDisciplina> disciplinasDoAluno = [];
                List<Nota> notasDoAluno = [];

                if(disciplinasDoPrimeiroSemestre != null)
                {
                    //Para cada disciplina do primeiro semestre do curso, será criado um objeto AlunoDisciplina e um objeto Nota.
                    foreach (var disciplina in disciplinasDoPrimeiroSemestre)
                    {
                        AlunoDisciplina alunoDisciplina = new ()
                        {
                            AlunoId = alunoCriado.Id,
                            DisciplinaId = disciplina.Id
                        };

                        Nota nota = new ()
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


                        disciplinasDoAluno.Add(alunoDisciplina);
                        notasDoAluno.Add(nota);
                    }
                }

                //tentativa de adicionar o aluno nas disciplinas do primeiro semestre do curso.
                try
                {
                    await _alunoDisciplinaRepository.AdicionarAlunoNasDisciplinas(disciplinasDoAluno);
                }
                catch (Exception ex)
                {
                    await _alunoRepository.DeletarAluno(alunoCriado);
                    return BadRequest($"Não foi possível adicionar o aluno nas disciplinas. Aluno excluído. Erro: {ex.Message}");
                }

                //tentativa de criar as notas do aluno.
                try
                {
                    await _notaRepository.CriarNotas(notasDoAluno);
                }
                catch (Exception ex)
                {
                    await _alunoRepository.DeletarAluno(alunoCriado);
                    return BadRequest($"Não foi possível criar grade de notas do aluno. Aluno excluído. Erro: {ex.Message}");
                }
                
                //Se tudo ocorrer bem, será retornado o DTO do aluno criado.
                var alunoDto = _mapper.Map<AlunoDto>(aluno);

                return Ok(alunoDto);
            }
            catch
            {
                return BadRequest("Não foi possível adicionar aluno!");
            }
        }


        [HttpPost("{alunoId}/rematricular")]
        [ProducesResponseType(200, Type = typeof(List<Disciplina>))]
        public async Task<IActionResult> RematriculaDoAluno([FromRoute] int alunoId, [FromBody] List<int> disciplinasIds)
        {
            try
            {
                var aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);
                var disciplinasAnteriores = await _alunoDisciplinaRepository.ObterDisciplinasDoAluno(alunoId);
                var notasDoAluno = await _alunoRepository.ObterNotasDoAluno(alunoId);

                if (aluno == null)
                    return NotFound("Aluno inexistente");

                if (aluno.StatusDoAluno != "Matriculado")
                    return BadRequest("O aluno não está matriculado.");

                List<AlunoDisciplina> disciplinasDesejadas = [];
                List<Nota> gradeDeNotas = [];

                foreach (var disciplinaId in disciplinasIds)
                {
                    var disciplina = await _disciplinaRepository.ObterDisciplinaPeloId(disciplinaId);

                    if (disciplina == null)
                        return NotFound("Disciplina inexistente");

                    var disciplinaJaCursada = disciplinasAnteriores.FirstOrDefault(dis => dis.Id == disciplinaId);
                    var notaDaDisciplinaJaCursada = notasDoAluno.FirstOrDefault(not => not.DisciplinaId == disciplinaId);

                    if (disciplinaJaCursada != null && (bool)notaDaDisciplinaJaCursada.Aprovado)
                        return BadRequest($"O aluno já foi aprovado na disciplina: {disciplinaJaCursada.NomeDaDisciplina}");

                    var alunoDisciplina =  new AlunoDisciplina()
                    {
                        AlunoId = alunoId,
                        DisciplinaId = disciplinaId
                    };

                    var notas = new Nota ()
                    {
                        AlunoId = alunoId,
                        DisciplinaId = disciplinaId
                    };


                    disciplinasDesejadas.Add(alunoDisciplina);
                    gradeDeNotas.Add(notas);
                }

                await _alunoDisciplinaRepository.AdicionarAlunoNasDisciplinas(disciplinasDesejadas);
                await _notaRepository.CriarNotas(gradeDeNotas);
 
                return Ok("Rematrícula realizada com sucesso!");
            }
            catch
            {
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

                if (!generos.Contains(alunoAtualizado.Genero, StringComparer.OrdinalIgnoreCase))
                    return BadRequest($"Insira um genêro válido: {string.Join(", ", generos)}.");

                if (!status.Contains(alunoAtualizado.StatusDoAluno, StringComparer.OrdinalIgnoreCase))
                    return BadRequest($"Insira uma situação válida: {string.Join(", ", status)}.");

                if (alunoAtualizado.Matricula != aluno.Matricula)
                    return BadRequest("O número de matrícula não pode ser alterado.");

                if (alunoAtualizado.Cpf != aluno.Cpf)
                    return BadRequest("O número de CPF não pode ser alterado.");

                if (alunoAtualizado.CursoId != aluno.CursoId)
                    return BadRequest("Não é possível alterar o curso!");

                if (alunoAtualizado.PeriodoDeIngresso != aluno.PeriodoDeIngresso)
                    return BadRequest ("Não é possível alterar o período de ingresso!");

                if (alunoAtualizado.PrimeiroNome == "" || alunoAtualizado.Sobrenome == "")
                    return BadRequest ("Preencha os campos de primeiro nome e sobrenome!");

                if(alunoAtualizado.DataDeNascimento != aluno.DataDeNascimento)
                    return BadRequest ("Não é possível alterar a data de nascimento!");
                

                aluno.PrimeiroNome = alunoAtualizado.PrimeiroNome;
                aluno.Sobrenome = alunoAtualizado.Sobrenome;
                aluno.NomeCompleto = $"{aluno.PrimeiroNome} {aluno.Sobrenome}";
                aluno.Endereco = alunoAtualizado.Endereco;
                aluno.EnderecoDeEmail = alunoAtualizado.EnderecoDeEmail;
                aluno.Genero = alunoAtualizado.Genero;
                aluno.StatusDoAluno = alunoAtualizado.StatusDoAluno;
  
                await _alunoRepository.AtualizarAluno(aluno);

                var alunoDto = _mapper.Map<AlunoDto>(aluno);

                return Ok("Informações alteradas com sucesso!");
            }

            catch (Exception ex) 
            {
                return BadRequest("Não foi possível editar o aluno. " + ex.Message);
            }
        }


    }
}
