using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        void Crear(TipoCuenta tipoCuenta);
    }

    public class RepositoriosTiposCuentas: IRepositorioTiposCuentas
    {

        private readonly string connectionstring;

        public RepositoriosTiposCuentas(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public void Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionstring);
            var id = connection.QuerySingle<int>($@"
                            INSERT INTO TiposCuentas (Nombre, UsuarioId, Orden) 
                            VALUES (@Nombre,@UsuarioId,0);
                            SELECT SCOPE_IDENTITY();"
                            , tipoCuenta);
            tipoCuenta.Id = id; 
        }
    }
}
