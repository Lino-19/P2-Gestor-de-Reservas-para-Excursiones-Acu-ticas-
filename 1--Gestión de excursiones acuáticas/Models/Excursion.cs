using System.ComponentModel.DataAnnotations;

namespace _1__Gestión_de_excursiones_acuáticas
{
    public class Excursion
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public decimal Precio { get; set; }

        public int MaxParticipantes { get; set; }
    }
}