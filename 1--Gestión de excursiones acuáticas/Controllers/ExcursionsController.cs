using Microsoft.AspNetCore.Mvc;

namespace _1__Gestión_de_excursiones_acuáticas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcursionsController : ControllerBase
    {
        private static readonly List<Excursion> excursiones = new List<Excursion>();

        [HttpGet]
        public ActionResult<IEnumerable<Excursion>> Get()
        {
            return Ok(excursiones);
        }

        [HttpGet("{id}")]
        public ActionResult<Excursion> Get(int id)
        {
            var excursion = excursiones.FirstOrDefault(e => e.Id == id);
            if (excursion == null)
            {
                return NotFound();
            }
            return Ok(excursion);
        }

        [HttpPost]
        public ActionResult<Excursion> Post([FromBody] Excursion nuevaExcursion)
        {
            nuevaExcursion.Id = excursiones.Count > 0 ? excursiones.Max(e => e.Id) + 1 : 1;
            excursiones.Add(nuevaExcursion);
            return CreatedAtAction(nameof(Get), new { id = nuevaExcursion.Id }, nuevaExcursion);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Excursion excursionActualizada)
        {
            var excursion = excursiones.FirstOrDefault(e => e.Id == id);
            if (excursion == null)
            {
                return NotFound();
            }

            excursion.Nombre = excursionActualizada.Nombre;
            excursion.Descripcion = excursionActualizada.Descripcion;
            excursion.Fecha = excursionActualizada.Fecha;
            excursion.Precio = excursionActualizada.Precio;
            excursion.MaxParticipantes = excursionActualizada.MaxParticipantes;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var excursion = excursiones.FirstOrDefault(e => e.Id == id);
            if (excursion == null)
            {
                return NotFound();
            }

            excursiones.Remove(excursion);
            return NoContent();
        }
    }
}