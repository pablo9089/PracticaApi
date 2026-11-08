using Microsoft.EntityFrameworkCore;
using static PracticaApi.model;

namespace PracticaApi
{
    public class AppDB: DbContext
    {
        public AppDB(DbContextOptions<AppDB> opciones) : base(opciones) { }
        
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        protected override void OnModelCreating(ModelBuilder mb)
        {
            //NO SE PERMITEN DOS CUENTAS CON EL MISMO CORREO
            mb.Entity<Usuario>().HasIndex(u => u.correo).IsUnique();

        }

    }
    }
