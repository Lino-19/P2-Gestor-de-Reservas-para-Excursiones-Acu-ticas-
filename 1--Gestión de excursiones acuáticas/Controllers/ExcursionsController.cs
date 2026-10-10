using _1__Gestión_de_excursiones_acuáticas.Data;
using _1__Gestión_de_excursiones_acuáticas.Models;
using _1__Gestión_de_excursiones_acuáticas.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace _1__Gestión_de_excursiones_acuáticas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcursionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExcursionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExcursionDto>> GetAll()
        {
            var excursiones = _context.Excursiones.Select(e => new ExcursionDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Fecha = e.Fecha,
                Precio = e.Precio,
                MaxParticipantes = e.MaxParticipantes
            }).ToList();

            return Ok(excursiones);
        }

        [HttpGet("{id}")]
        public ActionResult<ExcursionDto> GetById(int id)
        {
            var excursion = _context.Excursiones.Find(id);

            if (excursion == null)
            {
                return NotFound();
            }

            var excursionDto = new ExcursionDto
            {
                Id = excursion.Id,
                Nombre = excursion.Nombre,
                Descripcion = excursion.Descripcion,
                Fecha = excursion.Fecha,
                Precio = excursion.Precio,
                MaxParticipantes = excursion.MaxParticipantes
            };

            return Ok(excursionDto);
        }

        [HttpPost]
        public ActionResult<ExcursionDto> Create([FromBody] ExcursionCreateDto request)
        {
            var excursion = new Excursion
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Fecha = request.Fecha,
                Precio = request.Precio,
                MaxParticipantes = request.MaxParticipantes
            };

            _context.Excursiones.Add(excursion);
            _context.SaveChanges();

            var excursionDto = new ExcursionDto
            {
                Id = excursion.Id,
                Nombre = excursion.Nombre,
                Descripcion = excursion.Descripcion,
                Fecha = excursion.Fecha,
                Precio = excursion.Precio,
                MaxParticipantes = excursion.MaxParticipantes
            };

            return CreatedAtAction(nameof(GetById), new { id = excursion.Id }, excursionDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ExcursionCreateDto request)
        {
            var excursion = _context.Excursiones.Find(id);

            if (excursion == null)
            {
                return NotFound();
            }

            excursion.Nombre = request.Nombre;
            excursion.Descripcion = request.Descripcion;
            excursion.Fecha = request.Fecha;
            excursion.Precio = request.Precio;
            excursion.MaxParticipantes = request.MaxParticipantes;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var excursion = _context.Excursiones.Find(id);

            if (excursion == null)
            {
                return NotFound();
            }

            _context.Excursiones.Remove(excursion);
            _context.SaveChanges();

            return NoContent();
        }
    }
}