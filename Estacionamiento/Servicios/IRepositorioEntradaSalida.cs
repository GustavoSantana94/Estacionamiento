using Estacionamiento.Models;

namespace Estacionamiento.Servicios
{
    public interface IRepositorioEntradaSalida
    {
        Task<IEnumerable<EntradaSalidaDto>> ObtenereEntradasSalidas();
    }
}
