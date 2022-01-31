using Domain.Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class VendasRepository : IVendasRepository
    {
        public Task<IEnumerable<Vendas>> GetColecaoCarros()
        {
            throw new NotImplementedException();
        }
    }
}
