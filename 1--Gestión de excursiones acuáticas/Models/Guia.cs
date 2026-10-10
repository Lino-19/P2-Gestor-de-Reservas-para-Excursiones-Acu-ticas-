using System.ComponentModel.DataAnnotations;

namespace _1__Gestión_de_excursiones_acuáticas.Models
{
	public class Guia
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio.")]
		public string Nombre { get; set; } = string.Empty;

		[Required(ErrorMessage = "El apellido es obligatorio.")]
		public string Apellido { get; set; } = string.Empty;

		public string Telefono { get; set; } = string.Empty;

		[EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
		public string CorreoElectronico { get; set; } = string.Empty;

		public bool Disponible { get; set; } = true;
	}
}