using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
    }

    public class RepositoriosTiposCuentas : IRepositorioTiposCuentas
    {

        private readonly string connectionstring;

        public RepositoriosTiposCuentas(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionstring);
            var id = await connection.QuerySingleAsync<int>(
                @"
                INSERT INTO TiposCuentas (Nombre, UsuarioId, Orden) 
                VALUES (@Nombre,@UsuarioId,0);
                SELECT SCOPE_IDENTITY();"
                , tipoCuenta);
            tipoCuenta.Id = id;
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionstring);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"
                SELECT 1
                FROM TiposCuentas
                WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId",
                new {nombre, usuarioId});

            return existe == 1;
        }

        public async Task <IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionstring);
            return await connection.QueryAsync<TipoCuenta>(@"
                SELECT Id, Nombre, Orden
                FROM TiposCuentas
                WHERE UsuarioId = @UsuarioId", new {usuarioId});
            
               
        }
    }
}
