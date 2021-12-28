#region Manutenção do Código Fonte
/*
 <IDENTIFICACAO_DE_MANUTENCAO>
DATA            = "28/12/2021"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "Implementação inicial do AutoMapper"
</IDENTIFICACAO_DE_MANUTENCAO>
 */
#endregion

using AutoMapper;

namespace Infrastructure.AutoMapperProfile
{
    public class AutoMapperProfile
    {
        public static MapperConfiguration AutoMapperConfig()
        {
            return new MapperConfiguration(x =>
            {
                x.AddProfile<CadastroProfile>();
            });
        }
    }
}
