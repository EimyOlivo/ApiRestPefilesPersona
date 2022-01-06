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
        public UserService(IOptions<AppSettings> appSettings, IUsuarioRepo usuarioRepo)
        {
            _appSettings = appSettings.Value;
            _usuarioRepo = usuarioRepo;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _usuarioRepo.ObtenerUsuario(model.Usuario, model.Clave).Result;

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
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

        // helper methods

        private string generateJwtToken(Usuarios user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}