#region Manutenção do Código Fonte
/*

<IDENTIFICACAO_DE_MANUTENCAO>
DATA            = "28/12/2021"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "Implementação inicial"
</IDENTIFICACAO_DE_MANUTENCAO>

 
 */
#endregion

using Domain.Entities;
using FluentValidation;
using Infrastructure.Messages;

namespace Validations
{
    public class CadastroCarrosValidator:AbstractValidator<Carros>
    {
        public CadastroCarrosValidator()
        {
            RuleFor(x => x.carMarca).NotNull().WithMessage(Messages.CampoObrigatorio("Marca"));
        }
    }
}
