using Dapper;
using Estacionamiento.Models;
using Microsoft.Data.SqlClient;

namespace Estacionamiento.Servicios
{
    public class RepositorioEntradaSalida : IRepositorioEntradaSalida
    {
        private readonly String connectionString;
        public RepositorioEntradaSalida(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<EntradaSalidaDto>> ObtenereEntradasSalidas()
        {

            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<EntradaSalidaDto>("SP_OBTENER_ENTRADAS_SALIDAS");

        }
    }
}
