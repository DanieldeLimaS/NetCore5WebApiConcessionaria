using DataTransferObject.Cadastro;
using Domain.Entities;
using Domain.Infra;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Service
{
    public class CarrosService : ICarrosService
    {
        private readonly AppDbContext DbContext;
     
        public CarrosService(AppDbContext context)
        {
            DbContext = context;
        }



        public async Task<IEnumerable<Carros>> GetColecaoCarros()
        {
            try
            {
                    return await DbContext.Carros.ToListAsync();
                
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public async Task<IEnumerable<Carros>> GetColecaoCarrosFiltro(CarrosFiltroDTO filtro)
        {
            throw new NotImplementedException();
        }

        public Task<Carros> GetObjetoCarro(Guid carId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCarro(CarrosDTO objeto)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> CreateCarro(CarrosDTO objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCarro(Guid carId)
        {
            throw new NotImplementedException();
        }
    }
}
