#region Manutenção do Código Fonte
/*
< IDENTIFICACAO_DE_MANUTENCAO >
DATA            = "24/12/2021"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "alteração do filtro de getCarros e adição do summary no metodo get"
</IDENTIFICACAO_DE_MANUTENCAO>

<IDENTIFICACAO_DE_MANUTENCAO>
DATA            = "27/12/2021"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "Separando responsabilidades, levando metodo Get para camada de service para fazer a persistencia no banco"
</IDENTIFICACAO_DE_MANUTENCAO>
 
 */
#endregion

using AutoMapper;
using DataTransferObject.Cadastro;
using Domain.Entities;
using Infrastructure.Messages;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {
        ICarrosService iCarrosService;
        IMapper _mapper;
        public CarrosController(IMapper mapper)
        {
            iCarrosService = new CarrosService();
            _mapper = mapper;
        }

        // GET: api/Carros
        [HttpGet("BuscarTodos")]
        public async Task<IActionResult> GetCarros()
        {
            try
            {
                var lista = await iCarrosService.GetColecaoCarros();
                return Ok(lista.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.ErroPadrao + ex.Message);
            }
        }

        // GET: api/Carros?precoMenor=5000
        /// <summary>
        /// Busca lista de carros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [SwaggerOperation("Add a new Pet to the store")]
        [HttpGet("BuscarCarrosComFiltros")]
        public async Task<IActionResult> GetCarros([FromQuery] CarrosFiltroDTO filtro)
        {
            try
            {
                var colecaoCarro = await iCarrosService.GetColecaoCarrosFiltro(filtro);
                if (colecaoCarro == null)
                    return NotFound(Messages.NenhumRegistroLocalizado);

                return Ok(colecaoCarro);
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.ErroPadrao + ex.Message);
            }
        }

        // GET: api/Carros/5
        [HttpGet("BuscarCarroPorId/{id}")]
        public async Task<IActionResult> GetCarros(Guid id)
        {
            var carros = await iCarrosService.GetObjetoCarro(id);
            if (carros == null)
                return NotFound(Messages.NenhumRegistroLocalizado);

            return Ok(carros);
        }

        // PUT: api/Carros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("AtualizarCarro/{id}")]
        public async Task<IActionResult> PutCarros(Guid id, CarrosDTO carros)
        {
            if (!await iCarrosService.CarrosExists(id))
                return NotFound(Messages.RegistroNaoLocalizado);
           
            if (!await iCarrosService.UpdateCarro(_mapper.Map<Carros>(carros)))
                return BadRequest(Messages.ErroPadrao);
            return Ok(Messages.SucessoPadrao);
        }

        // POST: api/Carros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CadastrarCarros")]
        public async Task<IActionResult> PostCarros(List<CarrosDTO> carros)
        {
            try
            {
                if (!await iCarrosService.CreateCarro(_mapper.Map<List<Carros>>(carros)))
                    return BadRequest(Messages.ErroPadrao);
                return Ok(Messages.SucessoPadrao);
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.NaoFoiPossivelRealizarOperacao+ex.Message);
            }
        }

        // DELETE: api/Carros/5
        [HttpDelete("DeletarCarro/{id}")]
        public async Task<IActionResult> DeleteCarros(Guid id)
        {
            try
            {
                if (!await iCarrosService.CarrosExists(id))
                    return NotFound(Messages.RegistroNaoLocalizado);

                if (!await iCarrosService.DeleteCarro(id))
                    return BadRequest(Messages.ErroPadrao);

                return Ok(Messages.SucessoPadrao);
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.ErroPadrao+ex.Message);
            }
        }
    }
}
