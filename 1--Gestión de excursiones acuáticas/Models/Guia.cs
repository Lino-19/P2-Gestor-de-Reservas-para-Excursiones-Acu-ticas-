using System.ComponentModel.DataAnnotations;

namespace _1__Gestión_de_excursiones_acuáticas.Models
{
    public class Guia
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Especialidad { get; set; } = string.Empty;
    }
}