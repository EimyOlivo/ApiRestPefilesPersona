using ApiRestEimy.Modelos;


namespace ApiRestEimy.DTO
{
    public class AuthenticateResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Usuarios user, string token)
        {
            Username = user.Usuario;
            Token = token;
        }
    }
}