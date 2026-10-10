using _1__Gestión_de_excursiones_acuáticas.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1__Gestión_de_excursiones_acuáticas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private static readonly List<Reserva> _reservas = new List<Reserva>();

        [HttpGet]
        public ActionResult<IEnumerable<Reserva>> GetAll()
        {
            return Ok(_reservas);
        }

        [HttpGet("{id}")]
        public ActionResult<Reserva> GetById(int id)
        {
            var reserva = _reservas.FirstOrDefault(r => r.Id == id);
            if (reserva == null)
            {
                return NotFound("Reserva no encontrada.");
            }
            return Ok(reserva);
        }

        [HttpPost]
        public ActionResult<Reserva> Create(Reserva reserva)
        {
            decimal precioPorParticipante = 100.00m;
            reserva.MontoTotal = reserva.CantidadParticipantes * precioPorParticipante;

            reserva.Id = _reservas.Any() ? _reservas.Max(r => r.Id) + 1 : 1;
            reserva.FechaReserva = DateTime.Now;

            _reservas.Add(reserva);

            return CreatedAtAction(nameof(GetById), new { id = reserva.Id }, reserva);
        }

        [HttpPut("{id}/estado")]
        public IActionResult UpdateStatus(int id, [FromBody] string nuevoEstado)
        {
            var reserva = _reservas.FirstOrDefault(r => r.Id == id);
            if (reserva == null)
            {
                return NotFound("Reserva no encontrada.");
            }

            if (nuevoEstado != "Pendiente" && nuevoEstado != "Confirmada" && nuevoEstado != "Cancelada")
            {
                return BadRequest("Estado no válido. Use: Pendiente, Confirmada o Cancelada.");
            }

            reserva.Estado = nuevoEstado;
            return NoContent();
        }

        [HttpPut("{id}/pago")]
        public IActionResult RegistrarPago(int id, [FromBody] decimal montoAbono)
        {
            var reserva = _reservas.FirstOrDefault(r => r.Id == id);
            if (reserva == null)
            {
                return NotFound("Reserva no encontrada.");
            }

            if (montoAbono <= 0)
            {
                return BadRequest("El monto a abonar debe ser mayor a cero.");
            }

            if ((reserva.MontoPagado + montoAbono) > reserva.MontoTotal)
            {
                return BadRequest("El abono supera el saldo pendiente de la reserva.");
            }

            reserva.MontoPagado += montoAbono;
            return Ok(reserva);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reserva = _reservas.FirstOrDefault(r => r.Id == id);
            if (reserva == null)
            {
                return NotFound("Reserva no encontrada.");
            }

            _reservas.Remove(reserva);
            return NoContent();
        }
    }
}