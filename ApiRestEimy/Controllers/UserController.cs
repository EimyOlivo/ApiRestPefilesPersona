using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiRestEimy.Modelos;
using ApiRestEimy.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using ApiRestEimy.DTO;
using ApiRestEimy.Services;
using Microsoft.Extensions.Logging;
using System;

namespace ApiRestEimy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUsuarioRepo _usuario;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUsuarioRepo usuario, IMapper mapper, IUserService userService, ILogger<UserController> logger)
        {
            _usuario = usuario;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
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
            var context = HttpContext;
            Usuarios usuario = (Usuarios)context.Items["Usuario"];

            Usuarios perfiles = _mapper.Map<Usuarios>(nuevoUsuario);
            try
            {
                if (nuevoUsuario.Usuario == "" || nuevoUsuario.Clave == "")
                {
                    _logger.LogError("el usuario " + usuario.Id + " tuvo un error: Rellena bien el nombre y apellido al crear un perfil");
                    return BadRequest("La casilla de nombre y Apellido son obligatorias. Asegurece de llenarlas correctamenre");
                }

                await _usuario.Agregar(perfiles);
                _logger.LogInformation("el usuario " + usuario.Id + " registro un Post en la tabla PefilesPersona");
                return Ok(perfiles);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "error");
                return StatusCode(500);
            }
            
        }
    }
}
