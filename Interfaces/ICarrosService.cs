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
        Task<bool> CreateCarro(Carros objeto);
        Task<bool> UpdateCarro(Carros objeto);
        Task<bool> DeleteCarro(Guid carId);
        Task<bool> CarrosExists(Guid id);
    }
}
