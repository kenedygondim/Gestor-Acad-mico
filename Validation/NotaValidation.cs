using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Validation
{
    public static class NotaValidation
    {
        public static bool ValidarAtualizacaoDaNota(Nota nota, Nota notasRecebidas, out string errorMessage)
        {
            if(nota is null)
            {
                errorMessage = "Notas não encontradas";
                return false;
            } 
            if(notasRecebidas is null)
            {
                errorMessage = "Insira os novos valores para as notas.";
                return false;
            }

            InserirInformacoesEmOrdem(nota, notasRecebidas);

            errorMessage = string.Empty;
            return true;
        }

        public static void InserirInformacoesEmOrdem(Nota nota , Nota notasRecebidas)
        {
            nota.FrequenciaDoAluno = notasRecebidas.FrequenciaDoAluno;
            nota.PrimeiraAvaliacao = notasRecebidas.PrimeiraAvaliacao;
            nota.SegundaAvaliacao = notasRecebidas.SegundaAvaliacao;
            nota.Atividades = notasRecebidas.Atividades;
            nota.MediaGeral = ((nota.PrimeiraAvaliacao + nota.SegundaAvaliacao + nota.Atividades) / 3);
            nota.NotasFechadas = notasRecebidas.NotasFechadas;
            nota.Aprovado = VerificaAprovacao(nota);
        }

        public static bool VerificaAprovacao(Nota nota)
        {
            return nota.NotasFechadas && nota.MediaGeral > 6 && nota.FrequenciaDoAluno > 75;
        }

    }
}
