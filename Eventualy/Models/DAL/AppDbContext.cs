﻿using Eventualy.WEB.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventualy.WEB.Models.DAL
{
    public class AppDbContext:DbContext
    {
        //opciones como la cadena de conexión
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        //DbSet o representación de nuestras tablas
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Producto> Productos { get; set; }


    }
}
