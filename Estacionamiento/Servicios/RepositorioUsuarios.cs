using Dapper;
using Estacionamiento.Models;
using Microsoft.Data.SqlClient;

namespace Estacionamiento.Servicios
{
    public class RepositorioUsuarios: IRepositorioUsuarios
    {
        private readonly String connectionString;

        public RepositorioUsuarios(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearUsuario(Usuario usuario) {

           // usuario.EmailNormalizado = usuario.Email.ToUpper();


            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"
            INSERT INTO USUARIOS (Email,EmailNormalizado,PasswordHash)
            VALUES (@Email,@EmailNormalizado,@PasswordHash);
            SELECT SCOPE_IDENTITY();",usuario);
            return id;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(String emailNormalizado) {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<Usuario>(@"
                SELECT *FROM USUARIOS WHERE EmailNormalizado =@emailNormalizado", 
                new { emailNormalizado });

        }
    }
}
