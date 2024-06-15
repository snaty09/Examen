using Microsoft.EntityFrameworkCore;
using Examen.Models;


namespace Examen.Data
{
    public class HumanoContext : DbContext
    {
        public HumanoContext(DbContextOptions<HumanoContext> options) : base(options) { }

        public DbSet<Humano> Humanos { get; set; }
    }
}
