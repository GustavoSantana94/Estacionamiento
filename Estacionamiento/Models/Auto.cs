using System.ComponentModel.DataAnnotations;

namespace Estacionamiento.Models
{
    public class Auto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public String Placas { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Color { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Cliente { get; set; }

    }
}
