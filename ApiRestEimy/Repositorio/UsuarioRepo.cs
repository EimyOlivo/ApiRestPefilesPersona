using ApiRestEimy.Interfaces;
using ApiRestEimy.Modelos;
using ApiRestEimy.DB;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ApiRestEimy.Repositorio
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly PerfilPersonasContext _context;

        public UsuarioRepo(PerfilPersonasContext context)
        {
            _context = context;

        }

        public async Task<Usuarios> Agregar(Usuarios nuevousuario)
        {

            _context.Usuarios.Add(nuevousuario);
            await _context.SaveChangesAsync();

            return nuevousuario;
        }

        public async Task<IEnumerable<Usuarios>> ObtenerPerfiles()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
