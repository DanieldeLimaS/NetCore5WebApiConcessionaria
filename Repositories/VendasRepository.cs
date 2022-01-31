using Domain.Entities;
using Domain.Infra;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class VendasRepository : IVendasRepository
    {
        public async Task<IEnumerable<Vendas>> GetColecaoVendas()
        {
            using (var context = new AppDbContext())
            {
                return await context.Vendas.ToListAsync();
            }
        }

        public async Task<IEnumerable<Vendas>> GetColecaoVendasPorData(DateTime DataInicial, DateTime DataFinal)
        {
            using(var context = new AppDbContext())
            {
               return await context.Vendas.Where(x => x.venDataVenda >= DataInicial && x.venDataVenda <= DataFinal).ToListAsync();
            }
        }
    }
}
