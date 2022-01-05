using ApiRestEimy.DTO;
using ApiRestEimy.Repositorio;
using ApiRestEimy.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestEimy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IPerfilesPersonasRepo _perfilesPersonasRepo;
       
        public ApiController(IPerfilesPersonasRepo perfilesPersonasRepo)
        {
            _perfilesPersonasRepo = perfilesPersonasRepo;
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
            return await _perfilesPersonasRepo.ObtenerPerfil(id);

        }

        // POST api/<ApiController>
        [HttpPost]
        public async Task<ActionResult<PerfilesPersonas>> Post([FromBody]PerfilesPersonas nuevoperfil)
        {
            var perfil = await _perfilesPersonasRepo.Agregar(nuevoperfil);
            return CreatedAtAction(nameof(Get), new { id = perfil.Id }, perfil);
        }

        // PUT api/<ApiController>/5
   
    }
}
