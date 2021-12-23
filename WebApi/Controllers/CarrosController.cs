using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Infra;
using DataTransferObject.Cadastro;
using Interfaces;
using Service;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carros>>> GetCarros()
        {
           
            var lista = await iCarrosService.GetColecaoCarros();
            return lista.ToList();
        }

        // GET: api/Carros?precoMenor=5000
        [HttpGet("BuscarCarros")]

        public async Task<ActionResult<IEnumerable<Carros>>> GetCarros([FromQuery] CarrosFiltroDTO filtro)
        {
                var colecaoCarro = await _context.Carros.ToListAsync();
                colecaoCarro = CriaFiltroQueryColecao(filtro,colecaoCarro);
                return colecaoCarro;
            

        }

        private List<Carros> CriaFiltroQueryColecao(CarrosFiltroDTO filtro, List<Carros> query)
        {
            try
            {
                query = (filtro.carPrecoMenor.HasValue) ? (query.Where(x => x.carPreco <= filtro.carPrecoMenor).ToList()) : query;
                query = (filtro.carPrecoMaior.HasValue) ? (query.Where(x => x.carPreco <= filtro.carPrecoMaior).ToList()) : query;
                query = (filtro.carDataCadastroIni.HasValue) ? (query.Where(x => x.carDataCadastro >= filtro.carDataCadastroIni).ToList()) : query;
                query = (filtro.carDataCadastroFim.HasValue) ? (query.Where(x => x.carDataCadastro >= filtro.carDataCadastroFim).ToList()) : query;
                query = (filtro.carAnoIni.HasValue) ? (query.Where(x => x.carAno >= filtro.carAnoIni).ToList()) : query;
                query = (filtro.carAnoFim.HasValue) ? (query.Where(x => x.carAno >= filtro.carAnoFim).ToList()) : query;
                query = (string.IsNullOrWhiteSpace(filtro.carMarca)) ? (query.Where(x => x.carMarca.Contains(filtro.carMarca)).ToList()) : query;
                query = (string.IsNullOrWhiteSpace(filtro.carModelo)) ? (query.Where(x => x.carModelo.Contains(filtro.carModelo)).ToList()) : query;
                query = (filtro.carDisponivelVenda.HasValue) ? (query.Where(x => x.carDisponivel == filtro.carDisponivelVenda).ToList()) : query;

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        // GET: api/Carros/5
        [HttpGet("{id}")]
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
