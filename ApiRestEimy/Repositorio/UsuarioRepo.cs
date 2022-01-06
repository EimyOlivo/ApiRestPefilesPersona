using ApiRestEimy.Interfaces;
using ApiRestEimy.Modelos;
using ApiRestEimy.DB;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace ApiRestEimy.Repositorio
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly PerfilPersonasContext _context;
        //private readonly HttpContext _con;

        public UsuarioRepo(PerfilPersonasContext context)
        {
            _context = context;
            //_con = con;

        }

        public async Task<Usuarios> Agregar(Usuarios nuevousuario)
        {
            //var id = _con.Items["Usuario"].ToString();

            nuevousuario.Estatus = true;
            _context.Usuarios.Add(nuevousuario);
            await _context.SaveChangesAsync();
            
            return nuevousuario;
        }

        public async Task<IEnumerable<Usuarios>> ObtenerUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuarios> ObtenerUsuario(string usuario, string clave)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u=> u.Usuario == usuario && u.Clave == clave);
        }

        public async Task<Usuarios> ObtenerUsuarioId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
    }
}
