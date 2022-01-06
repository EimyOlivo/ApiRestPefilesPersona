using AutoMapper;
using ApiRestEimy.DTO;
using ApiRestEimy.Modelos;

namespace ApiRestEimy.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PerfilesPersonas, PerfilesPersonasCrearDTO>();
            CreateMap<PerfilesPersonasCrearDTO, PerfilesPersonas>();

            CreateMap<PerfilesPersonas, PerfilesPersonasDTO>();
            CreateMap<PerfilesPersonasDTO, PerfilesPersonas>();

            CreateMap<Usuarios, CrearUsuarioDTO>();
            CreateMap<CrearUsuarioDTO, Usuarios>();
        }
    }
}
