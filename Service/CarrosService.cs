#region Manutenção do Código Fonte
/*

<IDENTIFICACAO_DE_MANUTENCAO>
DATA            = "27/12/2021 Alt.1"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "Organização de controle de manutenção de fonte"
</IDENTIFICACAO_DE_MANUTENCAO>
 
<IDENTIFICACAO_DE_MANUTENCAO>
DATA            = "27/12/2021 Alt.2"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "Separando responsabilidades, levando metodo Get para camada de service para fazer a persistencia no banco"
</IDENTIFICACAO_DE_MANUTENCAO>

<IDENTIFICACAO_DE_MANUTENCAO>
DATA            = "28/12/2021 Alt.1"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "Adicionando summary nos metodos para documentar a responsabilidade de cada um"
</IDENTIFICACAO_DE_MANUTENCAO>
 */
#endregion

using DataTransferObject.Cadastro;
using Domain.Entities;
using Domain.Infra;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Service
{
    public class CarrosService : ICarrosService
    {
        /// <summary>
        /// Construtor Padrão da classe CarrosService
        /// </summary>
        public CarrosService()
        {

        }

        /// <summary>
        /// Obter toda a coleção de carros
        /// </summary>
        public async Task<IEnumerable<Carros>> GetColecaoCarros()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    return await context.Carros.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Obter uma coleção de objetos do tipo carros
        /// </summary>
        /// <param name="filtro"></param>
        public async Task<IEnumerable<Carros>> GetColecaoCarrosFiltro(CarrosFiltroDTO filtro)
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    List<Carros> query = await context.Carros.ToListAsync();
                    query = (filtro.carPrecoMenor.HasValue) ? (context.Carros.Where(x => x.carPreco <= filtro.carPrecoMenor).ToList()) : query;
                    query = (filtro.carPrecoMaior.HasValue) ? (context.Carros.Where(x => x.carPreco <= filtro.carPrecoMaior).ToList()) : query;
                    query = (filtro.carDataCadastroIni.HasValue) ? (context.Carros.Where(x => x.carDataCadastro >= filtro.carDataCadastroIni).ToList()) : query;
                    query = (filtro.carDataCadastroFim.HasValue) ? (context.Carros.Where(x => x.carDataCadastro >= filtro.carDataCadastroFim).ToList()) : query;
                    query = (filtro.carAnoIni.HasValue) ? (context.Carros.Where(x => x.carAno >= filtro.carAnoIni).ToList()) : query;
                    query = (filtro.carAnoFim.HasValue) ? (context.Carros.Where(x => x.carAno >= filtro.carAnoFim).ToList()) : query;
                    query = (filtro.carMarca != null) ? (context.Carros.Where(x => x.carMarca.Contains(filtro.carMarca)).ToList()) : query;
                    query = (filtro.carModelo != null) ? (context.Carros.Where(x => x.carModelo.Contains(filtro.carModelo)).ToList()) : query;
                    query = (filtro.carDisponivelVenda.HasValue) ? (context.Carros.Where(x => x.carDisponivel == filtro.carDisponivelVenda).ToList()) : query;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        
        /// <summary>
        /// Obter objeto carro
        /// </summary>
        /// <param name="carId">id do carro</param>
        public async Task<Carros> GetObjetoCarro(Guid carId)
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    return await context.Carros.FindAsync(carId);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza objeto carros
        /// </summary>
        /// <param name="objeto">Objeto carro do tipo Carros</param>
        public async Task<bool> UpdateCarro(Carros objeto)
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    //context.Entry(objeto).State = EntityState.Modified;
                    context.Carros.Update(objeto);
                    context.Entry(objeto).Property(x => x.carId).IsModified = false;
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        /// <summary>
        /// Valida se o Carro existe no banco de dados e retorna um boleano
        /// </summary>
        /// <param name="id"> id do carro</param>
        public async Task<bool> CarrosExists(Guid id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    return await context.Carros.AnyAsync(e => e.carId == id);
                }
            }
            catch(Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Realiza o cadastro do objeto no banco de dados
        /// </summary>
        /// <param name="objetos">Lista de objetos do tipo carro</param>
        public async Task<bool> CreateCarro(List<Carros> objetos)
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    context.Carros.AddRange(objetos);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Deleta objeto do banco de dados
        /// </summary>
        /// <param name="carId"> Id do carro</param>
        public async Task<bool> DeleteCarro(Guid carId)
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    var carro = await GetObjetoCarro(carId);
                    context.Carros.Remove(carro);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
           
        }
    }
}
