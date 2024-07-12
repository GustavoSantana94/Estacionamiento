using System.ComponentModel.DataAnnotations;

namespace Estacionamiento.Models
{
    public class Login
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Password { get; set; }

        public bool Recuerdame { get; set; }
    }
}
