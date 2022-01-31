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

<IDENTIFICACAO_DE_MANUTENCAO>
DATA            = "28/12/2021"
PROGRAMADOR     = "Daniel de Lima dos Santos"
MANUTENÇÃO      = "Adição do summary nos metodos para deixar documentado o que cada metodo realiza."
</IDENTIFICACAO_DE_MANUTENCAO>
 
 */
#endregion

using AutoMapper;
using DataTransferObject.Cadastro;
using Domain.Entities;
using Infrastructure.Messages;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Repositories;
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
        ICarrosRepository iCarrosRepository;
        IMapper _mapper;
        public CarrosController(IMapper mapper)
        {
            iCarrosRepository = new CarrosRepository();
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
                var lista = await iCarrosRepository.GetColecaoCarros();
                return Ok(lista.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.ErroPadrao + ex.Message);
            }
        }

        // GET: api/Carros?precoMenor=5000
        /// <summary>
        /// Requisição GET para listar todos os carros com o filtro informado
        /// </summary>
        /// <param name="filtro">filtro informado na api para realizar a consulta</param>
        /// <returns></returns>
        [SwaggerOperation("Add a new Pet to the store")]
        [HttpGet("BuscarCarrosComFiltros")]
        public async Task<IActionResult> GetCarros([FromQuery] CarrosFiltroDTO filtro)
        {
            try
            {
                var colecaoCarro = await iCarrosRepository.GetColecaoCarrosFiltro(filtro);
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
        /// <summary>
        /// Requisição GET para listar todos os carros com o ID informado
        /// </summary>
        /// <param name="id">Id do carro</param>
        /// <returns></returns>
        [HttpGet("BuscarCarroPorId/{id}")]
        public async Task<IActionResult> GetCarros(Guid id)
        {
            var carros = await iCarrosRepository.GetObjetoCarro(id);
            if (carros == null)
                return NotFound(Messages.NenhumRegistroLocalizado);

            return Ok(carros);
        }

        // PUT: api/Carros/5
        /// <summary>
        /// Requisição PUT para atualizar as informações do carro informado pelo ID
        /// </summary>
        /// <param name="id">ID do carro</param>
        /// <param name="carros">objeto carro</param>
        /// <returns></returns>
        [HttpPut("AtualizarCarro/{id}")]
        public async Task<IActionResult> PutCarros(Guid id, CarrosDTO carros)
        {
            if (!await iCarrosRepository.CarrosExists(id))
                return NotFound(Messages.RegistroNaoLocalizado);

            if (!await iCarrosRepository.UpdateCarro(_mapper.Map<Carros>(carros)))
                return BadRequest(Messages.ErroPadrao);
            return Ok(Messages.SucessoPadrao);
        }

        // POST: api/Carros
        /// <summary>
        /// Requisição POST para Cadastrar o carro/Lista de carros informado pelo usuário
        /// </summary>
        /// <param name="carros">Lista de carros do tipo DTO</param>
        /// <returns></returns>
        [HttpPost("CadastrarCarros")]
        public async Task<IActionResult> PostCarros(List<CarrosDTO> carros)
        {
            try
            {
                if (!await iCarrosRepository.CreateCarro(_mapper.Map<List<Carros>>(carros)))
                    return BadRequest(Messages.ErroPadrao);
                return Ok(Messages.SucessoPadrao);
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.NaoFoiPossivelRealizarOperacao + ex.Message);
            }
        }

        // DELETE: api/Carros/5
        /// <summary>
        /// Requisição DELETE para deletar o carro pelo ID informado no parametro pelo usuário
        /// </summary>
        /// <param name="id">ID do carro</param>
        /// <returns></returns>
        [HttpDelete("DeletarCarro/{id}")]
        public async Task<IActionResult> DeleteCarros(Guid id)
        {
            try
            {
                if (!await iCarrosRepository.CarrosExists(id))
                    return NotFound(Messages.RegistroNaoLocalizado);

                if (!await iCarrosRepository.DeleteCarro(id))
                    return BadRequest(Messages.ErroPadrao);

                return Ok(Messages.SucessoPadrao);
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.ErroPadrao + ex.Message);
            }
        }
    }
}
