using Microsoft.EntityFrameworkCore;
using semana10_Exercicios.Models;

namespace semana10_Exercicios.Context
{
    public class LocacaoContext : DbContext
    {
        public LocacaoContext(DbContextOptions<LocacaoContext> options) : base(options) 
        {
            
        }

        public DbSet<MarcaModel> Marcas { get; set; }
        public DbSet<CarroModel> Carros { get; set; }
    }
}
