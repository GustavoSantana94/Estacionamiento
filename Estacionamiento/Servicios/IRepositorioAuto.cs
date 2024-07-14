using Estacionamiento.Models;

namespace Estacionamiento.Servicios
{
    public interface IRepositorioAuto
    {
        Task ActualizarAuto(Auto auto);
        Task ActualizarEntrada(EntradaSalida entradaSalida);
        Task ActualizarSalida(int Id);
        Task BorrarAuto(int Id);
        Task CrearAuto(Auto auto);
        Task CrearEntrada(EntradaSalida entradaSalida);
        Task<bool> ExistePlacas(string placas);
        Task<Auto> ObtenerAutoPorId(int Id);
        Task<IEnumerable<Auto>> ObtenerAutos();
        Task<EntradaSalida> ObtenerEntradaSalidaPorId(int Id);
    }
}
