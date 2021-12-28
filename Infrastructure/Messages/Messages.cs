#region Manutenção do Código Fonte
/*

<IDENTIFICACAO_DE_MANUTENCAO>
DATA            = "28/12/2021"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "Implementação inicial"
</IDENTIFICACAO_DE_MANUTENCAO>

 
 */
#endregion

namespace Infrastructure.Messages
{
    public static class Messages
    {
        /// <summary>
        /// Registro não localizado.
        /// </summary>
        public static string RegistroNaoLocalizado => "Registro não localizado.";
        /// <summary>
        /// Ocorreu um erro interno ao processar a informação:
        /// </summary>
        public static string ErroPadrao => "Ocorreu um erro interno ao processar a informação:\n";

        public static string CampoObrigatorio(string campo)
        {
            return $"O campo {campo} é obrigatório.";
        }

        /// <summary>
        /// Não foi possivel realizar a operação:
        /// </summary>
        public static string NaoFoiPossivelRealizarOperacao => "Não foi possivel realizar a operação:\n Detalhes do erro: ";
        /// <summary>
        /// Nenhum registro localizado.
        /// </summary>
        public static string NenhumRegistroLocalizado => "Nenhum registro localizado.";
        /// <summary>
        /// Operação realizada com sucesso.
        /// </summary>
        public static string SucessoPadrao => "Operação realizada com sucesso.";
        /// <summary>
        /// Informe o ID do carro para realizar essa operação
        /// </summary>
        public static string InformeOIdDoCarro => "Informe o ID do carro para realizar essa operação";

    }
}
