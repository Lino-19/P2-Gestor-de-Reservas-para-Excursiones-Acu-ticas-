using _1__Gestión_de_excursiones_acuáticas.Models;
using Microsoft.EntityFrameworkCore;

namespace _1__Gestión_de_excursiones_acuáticas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Excursion> Excursiones { get; set; } = null!;
        public DbSet<Guia> Guias { get; set; } = null!;
        public DbSet<Reserva> Reservas { get; set; } = null!;
    }
}