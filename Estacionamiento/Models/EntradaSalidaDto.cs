using System.ComponentModel.DataAnnotations;

namespace Estacionamiento.Models
{
    public class EntradaSalidaDto
    {
        public int Id { get; set; }
        public String Placas { get; set; }

        public String FechaEntrada { get; set; }

        public String FechaSalida { get; set; }

        public String PrecioPorMinuto { get; set; }

        public String PrecioTotal { get; set; }
    }
}
