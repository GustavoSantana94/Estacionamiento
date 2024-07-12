using Estacionamiento.Models;

namespace Estacionamiento.Servicios
{
    public interface IRepositorioAuto
    {
        Task ActualizarAuto(Auto auto);
        Task BorrarAuto(int Id);
        Task CrearAuto(Auto auto);
        Task<bool> ExistePlacas(string placas);
        Task<Auto> ObtenerAutoPorId(int Id);
        Task<IEnumerable<Auto>> ObtenerAutos();
    }
}
