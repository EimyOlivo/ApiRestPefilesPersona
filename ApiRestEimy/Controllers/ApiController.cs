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


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestEimy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IPerfilesPersonasRepo _perfilesPersonasRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ApiController> _logger;

        public ApiController(IPerfilesPersonasRepo perfilesPersonasRepo, IMapper mapper, ILogger<ApiController> logger)
        {
            _perfilesPersonasRepo = perfilesPersonasRepo;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<ApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilesPersonas>>> Get()
        {
            try
            {
                return Ok(await _perfilesPersonasRepo.ObtenerPerfiles());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error");
                return Ok("");



            }
        }

        // GET api/<ApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilesPersonas>> Get(int id)
        {
            try
            {
                var perfil = await _perfilesPersonasRepo.ObtenerPerfil(id);
                return Ok("no existe el perfil numero " + id);
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
            try
            {
                PerfilesPersonas perfiles = _mapper.Map<PerfilesPersonas>(nuevoperfil);

                if (nuevoperfil.Nombre == "" || nuevoperfil.Apellido == "")
                {
                    _logger.LogError("Rellena bien el nombre y apellido al crear un perfil");
                    return BadRequest("La casilla de nombre y Apellido son obligatorias. Asegurece de llenarlas correctamenre");
                }
                await _perfilesPersonasRepo.Agregar(perfiles);
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
            try
            {
                if(id == null || persona == null || persona.Nombre == null || persona.Apellido == null)
                {
                    return BadRequest("Ingrese los datos de manera correcta");
                }

                PerfilesPersonas perfiles = _mapper.Map<PerfilesPersonas>(persona);
                perfiles.Id = id;
                //var perfilObtenido = _perfilesPersonasRepo.ObtenerPerfil(id);
                await _perfilesPersonasRepo.editar(perfiles);
                return Ok(perfiles);
                
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
    }
}