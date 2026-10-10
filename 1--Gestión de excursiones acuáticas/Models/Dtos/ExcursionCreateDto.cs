namespace _1__Gestión_de_excursiones_acuáticas.Models.Dtos
{
    public class ExcursionCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Precio { get; set; }
        public int MaxParticipantes { get; set; }
    }
}