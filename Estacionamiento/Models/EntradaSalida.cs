using System.ComponentModel.DataAnnotations;

namespace Estacionamiento.Models
{
    public class EntradaSalida
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Placas { get; set; }

        public int FechaEntrada { get; set; }

        public int FechaSalida { get; set; }
        
        public int PrecioPorMinuto { get; set; }

    }
}
