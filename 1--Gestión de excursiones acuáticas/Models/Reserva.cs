using System.ComponentModel.DataAnnotations;

namespace _1__Gestión_de_excursiones_acuáticas.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El ID de la excursión es obligatorio.")]
        public int ExcursionId { get; set; }

        public DateTime FechaReserva { get; set; } = DateTime.Now;

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de participantes debe ser al menos 1.")]
        public int CantidadParticipantes { get; set; }

        // Estados permitidos: Pendiente, Confirmada, Cancelada
        public string Estado { get; set; } = "Pendiente";

        public decimal MontoTotal { get; set; }

        public decimal MontoPagado { get; set; }

        public decimal SaldoPendiente => MontoTotal - MontoPagado;

        // Estado del pago: Pendiente, Parcialmente Pagada, Pagada
        public string EstadoPago
        {
            get
            {
                if (MontoPagado <= 0) return "Pendiente";
                if (MontoPagado < MontoTotal) return "Parcialmente Pagada";
                return "Pagada";
            }
        }
    }
}