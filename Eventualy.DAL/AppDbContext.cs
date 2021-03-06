using Eventualy.Model.Entities;
using Eventualy.Model.Entities.Usuarios;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        //opciones como la cadena de conexión
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //Esto lo utilizamos para llenar las tablas (seeder)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

        }

        //DbSet o representación de nuestras tablas
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
