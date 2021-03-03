using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelos;

namespace parcialAngular.Models
{
    public class BaseDatos : DbContext
    {
        public BaseDatos(DbContextOptions opciones):base (opciones)

        {

        }
        public DbSet<Modelos.usuarios> usuarios { get; set; }
        public DbSet<Modelos.eventos> eventos { get; set; }


    }
}
