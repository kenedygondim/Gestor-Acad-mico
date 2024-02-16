using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Validation
{
    public static class CursoValidation
    {

        readonly static string[] turnos = ["Matutino", "Vespertino", "Noturno", "Integral"];
        readonly static string[] categorias = ["Tecnólogo", "Bacharelado", "Licenciatura", "Pós-graduação", "Cursos livres"];
        readonly static string[] modalidades = ["Presencial", "EAD", "Híbrido"];

        public static bool ValidarCriacaoDoCurso(Curso curso, out string errorMessage)
        {
            if(curso is null)
            {
                errorMessage = "Insira os dados!";
                return false;
            }
            if(!turnos.Contains(curso.Turno, StringComparer.OrdinalIgnoreCase))
            {
                errorMessage = $"Insira um turno válido: {string.Join(", ", turnos)}";
                return false;
            }
            if (!categorias.Contains(curso.CategoriaDoCurso, StringComparer.OrdinalIgnoreCase))
            { 
                errorMessage = $"Insira uma categoria válida: {string.Join(", ", categorias)}";
                return false;
            }
            if (!modalidades.Contains(curso.Modalidade, StringComparer.OrdinalIgnoreCase))
            {
                errorMessage = $"Insira uma modalidade válida: {string.Join(", ", modalidades)}";
                return false;
            }
                
            errorMessage = string.Empty;
            return true;
        }


        public static bool ValidarAtualizacaoDoCurso(Curso curso, Curso cursoAtualizado, out string errorMessage)
        {
            if (curso == null)
            {
                errorMessage = "Curso inexistente";
                return false;
            }

            if (curso.Id != cursoAtualizado.Id)
            {
                errorMessage = "Ocorreu um erro na validação dos identificadores.";
                return false;
            }

            if (!turnos.Contains(cursoAtualizado.Turno, StringComparer.OrdinalIgnoreCase))
            {
                errorMessage = $"Insira um turno válido: {string.Join(", ", turnos)}";
                return false;
            }

            if (!categorias.Contains(cursoAtualizado.CategoriaDoCurso, StringComparer.OrdinalIgnoreCase))
            {
                errorMessage = $"Insira uma categoria válida: {string.Join(", ", categorias)}";
                return false;
            }

            if (!modalidades.Contains(cursoAtualizado.Modalidade, StringComparer.OrdinalIgnoreCase))
            {
                errorMessage = $"Insira uma modalidade válida: {string.Join(", ", modalidades)}";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

    }
}
