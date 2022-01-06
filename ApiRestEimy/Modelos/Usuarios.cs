using System.ComponentModel.DataAnnotations;

namespace ApiRestEimy.Modelos
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public bool Estatus { get; set; }
    }
}
