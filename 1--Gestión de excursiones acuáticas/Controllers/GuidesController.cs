using _1__Gestión_de_excursiones_acuáticas.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1__Gestión_de_excursiones_acuáticas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuidesController : ControllerBase
    {
        private static readonly List<Guia> _guias = new List<Guia>();

        [HttpGet]
        public ActionResult<IEnumerable<Guia>> GetAll()
        {
            return Ok(_guias);
        }

        [HttpGet("{id}")]
        public ActionResult<Guia> GetById(int id)
        {
            var guia = _guias.FirstOrDefault(g => g.Id == id);
            if (guia == null)
            {
                return NotFound("Guía no encontrado.");
            }
            return Ok(guia);
        }

        [HttpPost]
        public ActionResult<Guia> Create(Guia guia)
        {
            guia.Id = _guias.Any() ? _guias.Max(g => g.Id) + 1 : 1;
            _guias.Add(guia);

            return CreatedAtAction(nameof(GetById), new { id = guia.Id }, guia);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Guia guia)
        {
            var guiaExistente = _guias.FirstOrDefault(g => g.Id == id);
            if (guiaExistente == null)
            {
                return NotFound("Guía no encontrado.");
            }

            guiaExistente.Nombre = guia.Nombre;
            guiaExistente.Apellido = guia.Apellido;
            guiaExistente.Telefono = guia.Telefono;
            guiaExistente.CorreoElectronico = guia.CorreoElectronico;
            guiaExistente.Disponible = guia.Disponible;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var guia = _guias.FirstOrDefault(g => g.Id == id);
            if (guia == null)
            {
                return NotFound("Guía no encontrado.");
            }

            _guias.Remove(guia);
            return NoContent();
        }
    }
}
