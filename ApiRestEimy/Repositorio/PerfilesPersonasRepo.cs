using System;
using ApiRestEimy.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestEimy.Modelos;
using ApiRestEimy.DB;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ApiRestEimy.Repositorio
{
    public class PerfilesPersonasRepo : IPerfilesPersonasRepo
    {
        private readonly PerfilPersonasContext _context;
        private readonly IMapper _mapper;
        public PerfilesPersonasRepo(PerfilPersonasContext context)
        {
            _context = context;

        }

        public async Task<PerfilesPersonas> Agregar(PerfilesPersonas nuevoperfil)
        {

            _context.PerfilPersonas.Add(nuevoperfil);
            await _context.SaveChangesAsync();

            return nuevoperfil;
        }

        public async Task editar(PerfilesPersonas perfil)
        {
            _context.Entry(perfil).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task eliminar(int id)
        {
            var eliminarperfil = await _context.PerfilPersonas.FindAsync(id);
            _context.PerfilPersonas.Remove(eliminarperfil);
            await _context.SaveChangesAsync();

        }

        public async Task<PerfilesPersonas> ObtenerPerfil(int id)
        {
            return await _context.PerfilPersonas.FindAsync(id);
        }

        public async Task<IEnumerable<PerfilesPersonas>> ObtenerPerfiles()
        {
            return await _context.PerfilPersonas.ToListAsync();
        }

        /*public static List<PerfilesPersonas> listaperfiles = new List<PerfilesPersonas>()
        {
            new PerfilesPersonas () {Nombre = "Maria", Apellido ="Rodrigez", Edad = 27},
            new PerfilesPersonas () {Nombre = "Nelson", Apellido ="Lopez", Edad = 23},
            new PerfilesPersonas () {Nombre = "Natasha", Apellido ="Vargas", Edad = 17}

        };

        public IEnumerable<PerfilesPersonas> ObtenerPerfiles()
        {
            return listaperfiles;
        }

        public PerfilesPersonas Obtenerperfil(int id)
        {
            var perfilpersona = listaperfiles.Where(perfil => perfil.Id == id);

            return perfilpersona.FirstOrDefault();
        }

        public void Agregar(PerfilesPersonas nuevoPerfil)
        {
            listaperfiles.Add(nuevoPerfil);
        }

        public void eliminar(int id)
        {
            var perfilpersona = listaperfiles.Remove();

        }

        //public PerfilesPersonas editarperfil(int id)
        //{
          //  var perfilpersona = listaperfiles.Where(perfil => perfil.Id == id).FirstOrDefault();

         
        //}*/

    }
}
