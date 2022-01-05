using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiRestEimy.Modelos;

namespace ApiRestEimy.DB
{
    public class PerfilPersonasContext: DbContext
    {
        public PerfilPersonasContext(DbContextOptions<PerfilPersonasContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<PerfilesPersonas> PerfilPersonas { get; set; }
    }
}
