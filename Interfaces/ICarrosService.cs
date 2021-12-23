using DataTransferObject.Cadastro;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICarrosService
    {
        Task<IEnumerable<Carros>> GetColecaoCarros();
        Task<IEnumerable<Carros>> GetColecaoCarrosFiltro(CarrosFiltroDTO filtro);
        Task<Carros> GetObjetoCarro(Guid carId);
        Task<bool> CreateCarro(CarrosDTO objeto);
        Task<bool> UpdateCarro(CarrosDTO objeto);
        Task<bool> DeleteCarro(Guid carId);
    }
}
