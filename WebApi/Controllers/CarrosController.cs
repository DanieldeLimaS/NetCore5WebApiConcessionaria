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

using DataTransferObject.Cadastro;
using Domain.Entities;
using Domain.Infra;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext _context;
        ICarrosService iCarrosService;
        public CarrosController(AppDbContext context)
        {
            _context = context;
            iCarrosService = new CarrosService(context);
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
                return BadRequest("Ocorreu um erro:\n" + ex.Message);
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

                return Ok(colecaoCarro.Count() > 0 ? colecaoCarro : "Nenhum registro localizado.");
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro:\n" + ex.Message);
            }

        }


        // GET: api/Carros/5
        [HttpGet("BuscarCarrosPorId/{id}")]
        public async Task<ActionResult<Carros>> GetCarros(Guid id)
        {
            var carros = await _context.Carros.FindAsync(id);

            if (carros == null)
            {
                return NotFound();
            }

            return carros;
        }

        // PUT: api/Carros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarros(Guid id, Carros carros)
        {
            if (id != carros.carId)
            {
                return BadRequest();
            }

            _context.Entry(carros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carros>> PostCarros(Carros carros)
        {
            _context.Carros.Add(carros);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarros", new { id = carros.carId }, carros);
        }

        // DELETE: api/Carros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarros(Guid id)
        {
            var carros = await _context.Carros.FindAsync(id);
            if (carros == null)
            {
                return NotFound();
            }

            _context.Carros.Remove(carros);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrosExists(Guid id)
        {
            return _context.Carros.Any(e => e.carId == id);
        }
    }
}
