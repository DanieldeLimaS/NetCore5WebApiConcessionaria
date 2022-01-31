using DataTransferObject.Cadastro;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IVendasRepository
    {
        Task<IEnumerable<Vendas>> GetColecaoVendas();
        Task<IEnumerable<Vendas>> GetColecaoVendasPorData(DateTime DataInicial, DateTime DataFinal);
      
    }
}
