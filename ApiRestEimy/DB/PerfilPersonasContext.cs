using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiRestEimy.Modelos;
using ApiRestEimy.DTO;

namespace ApiRestEimy.DB
{
    public class PerfilPersonasContext: DbContext
    {
        public PerfilPersonasContext(DbContextOptions<PerfilPersonasContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<PerfilesPersonas> PerfilPersonas { get; set; }
        
    }
}
