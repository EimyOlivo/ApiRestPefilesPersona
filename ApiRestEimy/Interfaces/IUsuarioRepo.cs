using ApiRestEimy.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRestEimy.Interfaces
{
    public interface IUsuarioRepo
    {
        Task<IEnumerable<Usuarios>> ObtenerPerfiles();
        Task<Usuarios> Agregar(Usuarios nuevoperfil);
    }
}
