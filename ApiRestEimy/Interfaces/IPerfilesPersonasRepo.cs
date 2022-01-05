using ApiRestEimy.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApiRestEimy
{
    public interface IPerfilesPersonasRepo
    {
        Task<IEnumerable<PerfilesPersonas>> ObtenerPerfiles();
        Task<PerfilesPersonas> ObtenerPerfil(int id);
        Task<PerfilesPersonas> Agregar(PerfilesPersonas nuevoperfil);
        Task editar(PerfilesPersonas perfil);
        Task eliminar(int id);

    }
}
