using System.ComponentModel.DataAnnotations;

namespace Estacionamiento.Models
{
    public class EntradaSalida
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public String Placas { get; set; }

        public DateTime FechaEntrada { get; set; }

        public DateTime FechaSalida { get; set; }
        
        public String PrecioPorMinuto { get; set; }

    }
}
