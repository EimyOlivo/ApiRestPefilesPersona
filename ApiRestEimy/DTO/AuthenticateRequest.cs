using System.ComponentModel.DataAnnotations;

namespace ApiRestEimy.DTO
{
    public class AuthenticateRequest
    {
        
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}