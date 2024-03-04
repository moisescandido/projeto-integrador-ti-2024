using Microsoft.EntityFrameworkCore;
using projeto.Models;

namespace Contatos.Models
{
    public class ClimaContexto : DbContext
    {
        public ClimaContexto(DbContextOptions<ClimaContexto> options) : base(options)
        {
        }

        public DbSet<Clima> Clima { get; set; }
    }
}