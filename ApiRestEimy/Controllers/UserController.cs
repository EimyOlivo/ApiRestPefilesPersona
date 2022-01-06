using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiRestEimy.Modelos;
using ApiRestEimy.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using ApiRestEimy.DTO;
using ApiRestEimy.Services;

namespace ApiRestEimy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUsuarioRepo _usuario;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUsuarioRepo usuario, IMapper mapper, IUserService userService)
        {
            _usuario = usuario;
            _mapper = mapper;
            _userService = userService;
        }

        // POST api/<ApiController>
        [HttpPost("autenticar")]
        public IActionResult Autenticar(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "El usuario o la clave es incorrecta" });

            return Ok(response);
        }

        // GET: api/<ApiController>
       [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> Get()
        {
            return Ok(await _usuario.ObtenerUsuarios());
            
        }

        // POST api/<ApiController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Usuarios>> Post([FromBody] CrearUsuarioDTO nuevoUsuario)
        {
            Usuarios perfiles = _mapper.Map<Usuarios>(nuevoUsuario);

            await _usuario.Agregar(perfiles);
            return Ok(perfiles);

        }
    }
}
