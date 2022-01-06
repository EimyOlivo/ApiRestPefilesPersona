using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiRestEimy.Modelos;
using ApiRestEimy.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using ApiRestEimy.DTO;

namespace ApiRestEimy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUsuarioRepo _usuario;
        private readonly IMapper _mapper;

        public UserController(IUsuarioRepo usuario, IMapper mapper)
        {
            _usuario = usuario;
            _mapper = mapper;
        }

        // GET: api/<ApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> Get()
        {
            return Ok(await _usuario.ObtenerPerfiles());
            
        }

        // POST api/<ApiController>
        [HttpPost]
        public async Task<ActionResult<Usuarios>> Post([FromBody] CrearUsuarioDTO nuevoUsuario)
        {
            Usuarios perfiles = _mapper.Map<Usuarios>(nuevoUsuario);

            await _usuario.Agregar(perfiles);
            return Ok(perfiles);



        }
    }
}
