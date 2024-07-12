using Dapper;
using Estacionamiento.Models;
using Microsoft.Data.SqlClient;
using System.Drawing;

namespace Estacionamiento.Servicios
{
    public class RepositorioAuto : IRepositorioAuto
    {
        private readonly String connectionString;
        public RepositorioAuto(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CrearAuto(Auto auto) {

            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                ("SP_INSERTAR_AUTO", new { Placas =auto.Placas, Color =auto.Color,Cliente =auto.Cliente },
                commandType: System.Data.CommandType.StoredProcedure);

            auto.Id = id;
            
        }

        public async Task<bool> ExistePlacas(String placas)
        {

            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
                (@"SELECT 1 FROM AUTOS WHERE Placas =@Placas;",new { placas});

            return existe == 1;

        }

        public async Task<IEnumerable<Auto>> ObtenerAutos() {

            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Auto>(@" SELECT Id,Placas,Color,Cliente FROM AUTOS");

        }

        public async Task ActualizarAuto(Auto auto) {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE SET Color=@Color, Placas =@Placas, Cliente=@Cliente WHERE ID =@Id", auto);
        }

        public async Task<Auto> ObtenerAutoPorId(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Auto>(@"SELECT Id,Placas,Color,Cliente FROM Autos WHERE Id =@Id ", new { Id });
        }

        public async Task BorrarAuto(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM Autos WHERE Id =@Id", new { Id });
        }
    }
}
