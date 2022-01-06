using ApiRestEimy.DTO;
using ApiRestEimy.Repositorio;
using ApiRestEimy.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNetCore.Http;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestEimy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ApiController : ControllerBase
    {
        private readonly IPerfilesPersonasRepo _perfilesPersonasRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ApiController> _logger;
        //private readonly HttpContext _context;

        public ApiController(IPerfilesPersonasRepo perfilesPersonasRepo, IMapper mapper, ILogger<ApiController> logger)
        {
            _perfilesPersonasRepo = perfilesPersonasRepo;
            _mapper = mapper;
            _logger = logger;
            //_context = context;
        }

        // GET: api/<ApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilesPersonas>>> Get()
        {
            var context = HttpContext;
            Usuarios usuario = (Usuarios)context.Items["Usuario"];
            try
            {
                _logger.LogInformation("el usuario " + usuario.Id + " registro un Get en la tabla PefilesPersona");
                return Ok(await _perfilesPersonasRepo.ObtenerPerfiles());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error");
                return StatusCode(500);
            }
        }

        // GET api/<ApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilesPersonas>> Get(int id)
        {
            var context = HttpContext;
            Usuarios usuario = (Usuarios)context.Items["Usuario"];
            try
            {
                if(id <= 0)
                {
                    _logger.LogError("el usuario " + usuario.Id + " el elemente llamado en get id perfil no existe");
                    return Ok("El perfil llamado no existe");
                }
                _logger.LogInformation("el usuario " + usuario.Id + " registro un Get por Id en la tabla PefilesPersona");
                var perfil = await _perfilesPersonasRepo.ObtenerPerfil(id);
                if(perfil == null)
                {
                    return NotFound("El Id dado no existe");
                }
                return Ok(perfil);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error");
                return BadRequest("Debe revisar los datos agregados");
            }            

        }

        // POST api/<ApiController>
        [HttpPost]
        public async Task<ActionResult<PerfilesPersonasCrearDTO>> Post([FromBody] PerfilesPersonasCrearDTO nuevoperfil)
        {
            var context = HttpContext;
            Usuarios usuario = (Usuarios)context.Items["Usuario"];
            try
            {
                PerfilesPersonas perfiles = _mapper.Map<PerfilesPersonas>(nuevoperfil);

                if (nuevoperfil.Nombre == "" || nuevoperfil.Apellido == "")
                {
                    _logger.LogError("el usuario " + usuario.Id + " tuvo un error: Rellena bien el nombre y apellido al crear un perfil");
                    return BadRequest("La casilla de nombre y Apellido son obligatorias. Asegurece de llenarlas correctamenre");
                }
                await _perfilesPersonasRepo.Agregar(perfiles);
                _logger.LogInformation("el usuario " + usuario.Id + " registro un Post en la tabla PefilesPersona");
                return Ok(perfiles);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "error");
                return StatusCode(500);
            }
           

        }

        // PUT api/<ApiController>/5
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] PerfilesPersonasCrearDTO persona)
        {
            var context = HttpContext;
            Usuarios usuario = (Usuarios)context.Items["Usuario"];

            var perfil = await _perfilesPersonasRepo.ObtenerPerfil(id);
            if (perfil == null)
            {
                return NotFound("El Id dado no existe");
            }

            try
            {
                if(id <= 0 || persona == null || persona.Nombre == null || persona.Apellido == null)
                {
                    _logger.LogError("el usuario " + usuario.Id + "A ingresado los datos de manero incorrecta");
                    return BadRequest("Ingrese los datos de manera correcta");
                }

                //PerfilesPersonas perfiles = _mapper.Map<PerfilesPersonas>(persona);
                perfil.Nombre = persona.Nombre;
                perfil.Apellido = persona.Apellido;
                perfil.Edad = persona.Edad;
                //perfiles.Id = id;
                //var perfilObtenido = _perfilesPersonasRepo.ObtenerPerfil(id);
                await _perfilesPersonasRepo.editar(perfil);
                _logger.LogInformation("el usuario " + usuario.Id + " a editado en la tabla PefilesPersona");
                return Ok(perfil);
                
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e, "error");
                return NotFound("el id seleccionado no existe ");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error");
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var context = HttpContext;
            Usuarios usuario = (Usuarios)context.Items["Usuario"];
            var perdilEliminar = await _perfilesPersonasRepo.ObtenerPerfil(id);
            if (perdilEliminar == null)
            {
                _logger.LogInformation("el usuario " + usuario.Id + "No se a encontrado el perfil seleccionado para poder eliminarlo");
                return NotFound("No se a encontrado el perfil seleccionado");

            }
            await _perfilesPersonasRepo.eliminar(perdilEliminar.Id);
            _logger.LogInformation("el usuario " + usuario.Id + " a eliminado con exito el perfil: " + id);
            return Ok("Se ha eliminado con existo");
        }

    }
}