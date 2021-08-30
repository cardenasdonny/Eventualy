using Eventualy.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.DAL
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoDocumento>().HasData(
                new TipoDocumento
                {
                    TipoDocumentoId = 1,
                    Nombre = "CC"
                },
                new TipoDocumento
                {
                    TipoDocumentoId = 2,
                    Nombre = "TI"
                },
                new TipoDocumento
                {
                    TipoDocumentoId = 3,
                    Nombre = "CE"
                }
            );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    ClienteId = 1,
                    Nombres = "Dony Cardenas",
                    TipoDocumentoId = 1,
                    Documento = "72284820",
                    Email = "cardenasdonny@gmail.com",
                    Estado = true
                }
            );
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    ClienteId = 2,
                    Nombres = "Sara Sofia",
                    TipoDocumentoId = 2,
                    Documento = "77889966",
                    Email = "sarasofi@gmail.com",
                    Estado = true
                }
            );
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    ClienteId = 3,
                    Nombres = "Samuel",
                    TipoDocumentoId = 3,
                    Documento = "998822",
                    Email = "samuel@gmail.com",
                    Estado = true
                }
            );

        }

    }
}
