using System.ComponentModel;

namespace Enums
{
    public enum EDisponivel: int
    {
        [Description("Disponível")]
        DISPONIVEL=0,
        [Description("Indisponível")]
        INDISPONIVEL=1
    }
}
