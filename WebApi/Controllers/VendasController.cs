using AutoMapper;
using Infrastructure.Messages;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class VendasController : Controller
    {
        IVendasRepository iVendaSRepository;
        IMapper _mapper;
        public VendasController(IMapper mapper)
        {
            iVendaSRepository = new VendasRepository();
            _mapper = mapper;
        }

        // GET: api/Carros
        /// <summary>
        /// Requisição GET para listar todos os carros
        /// </summary>
        [HttpGet("BuscarTodos")]
        public async Task<IActionResult> GetCarros()
        {
            try
            {
                var lista = await iVendaSRepository.GetColecaoVendas();
                return Ok(lista.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.ErroPadrao + ex.Message);
            }
        }
        // GET: api/Carros
        /// <summary>
        /// Requisição GET para listar todos os carros
        /// </summary>
        [HttpGet("BuscarVendaPorData")]
        public async Task<IActionResult> GetCarros([FromQuery]DateTime DataInicial, [FromQuery] DateTime DataFinal)
        {
            try
            {
                var lista = await iVendaSRepository.GetColecaoVendasPorData(DataInicial,DataFinal);
                return Ok(lista.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.ErroPadrao + ex.Message);
            }
        }
    }
}
