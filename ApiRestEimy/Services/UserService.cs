using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ApiRestEimy.DTO;
using ApiRestEimy.Helper;
using Microsoft.Extensions.Logging;
using ApiRestEimy.Modelos;
using ApiRestEimy.Repositorio;
using ApiRestEimy.Interfaces;

namespace ApiRestEimy.Services
{

    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<Usuarios> GetAll();
        Usuarios GetById(int id);
    }

    public class UserService : IUserService
    {
      

        private readonly AppSettings _appSettings;
        private readonly IUsuarioRepo _usuarioRepo;
        private readonly ILogger<UserService> _logger;
        public UserService(IOptions<AppSettings> appSettings, IUsuarioRepo usuarioRepo, ILogger<UserService> logger)
        {
            _appSettings = appSettings.Value;
            _usuarioRepo = usuarioRepo;
            _logger = logger;
        }


        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _usuarioRepo.ObtenerUsuario(model.Usuario, model.Clave).Result;
            
            
            if (user == null || user.Estatus == false) return null;

            var token = generateJwtToken(user);



            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<Usuarios> GetAll()
        {
            return _usuarioRepo.ObtenerUsuarios().Result;
        }

        public Usuarios GetById(int id)
        {
            return _usuarioRepo.ObtenerUsuarioId(id).Result;
        }


        private string generateJwtToken(Usuarios user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _logger.LogInformation("El user:" + user.Id + "Se creo el token: " + tokenHandler.WriteToken(token));
            return (tokenHandler.WriteToken(token));
        }

    }
}