﻿#region Manutenção do Código Fonte
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
 */
#endregion

using DataTransferObject.Cadastro;
using Domain.Entities;
using Domain.Infra;
using Infrastructure.String_Message;
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
        private readonly AppDbContext DbContext;
        public CarrosService()
        {

        }
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
            try
            {
                List<Carros> query = new List<Carros>();
                query = (filtro.carPrecoMenor.HasValue) ? (query.Where(x => x.carPreco <= filtro.carPrecoMenor).ToList()) : query;
                query = (filtro.carPrecoMaior.HasValue) ? (query.Where(x => x.carPreco <= filtro.carPrecoMaior).ToList()) : query;
                query = (filtro.carDataCadastroIni.HasValue) ? (query.Where(x => x.carDataCadastro >= filtro.carDataCadastroIni).ToList()) : query;
                query = (filtro.carDataCadastroFim.HasValue) ? (query.Where(x => x.carDataCadastro >= filtro.carDataCadastroFim).ToList()) : query;
                query = (filtro.carAnoIni.HasValue) ? (query.Where(x => x.carAno >= filtro.carAnoIni).ToList()) : query;
                query = (filtro.carAnoFim.HasValue) ? (query.Where(x => x.carAno >= filtro.carAnoFim).ToList()) : query;
                query = (filtro.carMarca != null) ? (query.Where(x => x.carMarca.Contains(filtro.carMarca)).ToList()) : query;
                query = (filtro.carModelo != null) ? (query.Where(x => x.carModelo.Contains(filtro.carModelo)).ToList()) : query;
                query = (filtro.carDisponivelVenda.HasValue) ? (query.Where(x => x.carDisponivel == filtro.carDisponivelVenda).ToList()) : query;

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        public async Task<Carros> GetObjetoCarro(Guid carId)
        {
            try
            {
                return await DbContext.Carros.FindAsync(carId);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public async Task<bool> UpdateCarro(Carros objeto)
        {
            try
            {
                DbContext.Entry(objeto).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public async Task<bool> CarrosExists(Guid id)
        {
            return await DbContext.Carros.AnyAsync(e => e.carId == id);
        }
        public async Task<bool> CreateCarro(Carros objeto)
        {
            try
            {
                DbContext.Carros.Add(objeto);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public async Task<bool> DeleteCarro(Guid carId)
        {
            try
            {
                var carro = await GetObjetoCarro(carId);
                DbContext.Carros.Remove(carro);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}
