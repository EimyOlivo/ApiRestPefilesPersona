using ApiRestEimy.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRestEimy.Interfaces
{
    public interface IUsuarioRepo
    {
        Task<IEnumerable<Usuarios>> ObtenerUsuarios();
        Task<Usuarios> Agregar(Usuarios nuevoperfil);
        Task<Usuarios> ObtenerUsuarioId(int id);
        Task<Usuarios> ObtenerUsuario(string usuario, string clave);

    }
}
