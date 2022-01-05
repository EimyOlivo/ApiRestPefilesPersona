using ApiRestEimy.DTO;
using ApiRestEimy.Repositorio;
using ApiRestEimy.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestEimy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IPerfilesPersonasRepo _perfilesPersonasRepo;
        private readonly IMapper _mapper;

        public ApiController(IPerfilesPersonasRepo perfilesPersonasRepo, IMapper mapper)
        {
            _perfilesPersonasRepo = perfilesPersonasRepo;
            _mapper = mapper;

        }
        // GET: api/<ApiController>
        [HttpGet]
        public async Task<IEnumerable<PerfilesPersonas>> Get()
        {
            return await _perfilesPersonasRepo.ObtenerPerfiles();

            
        }

        // GET api/<ApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerfilesPersonas>> Get(int id)
        {
            var perfil = await _perfilesPersonasRepo.ObtenerPerfil(id);
            if (perfil == null)
            {
                return NotFound("no existe el perfil numero " + id);
            }
            return Ok(perfil);

        }

        // POST api/<ApiController>
        [HttpPost]
        public async Task<ActionResult<PerfilesPersonasCrearDTO>> Post([FromBody] PerfilesPersonasCrearDTO nuevoperfil)
        {
            PerfilesPersonas perfiles = _mapper.Map<PerfilesPersonas>(nuevoperfil);

            //var perfil = await _perfilesPersonasRepo.Agregar(nuevoperfil);
            if(nuevoperfil.Nombre == "" || nuevoperfil.Apellido == "")
            {
                return NotFound("La casilla de nombre y Apellido son obligatorias. Asegurece de llenarlas correctamenre");
            }
            await _perfilesPersonasRepo.Agregar(perfiles);
            return Ok();


        }
        // PUT api/<ApiController>/5
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] PerfilesPersonas persona)
        {
            if (id != persona.Id)
            {
                return NotFound("No existe el perfil numero " + id);
            }
            PerfilesPersonas perfiles = _mapper.Map<PerfilesPersonas>(persona);
            await _perfilesPersonasRepo.editar(perfiles);
            return NoContent();
           
        }
    }
}