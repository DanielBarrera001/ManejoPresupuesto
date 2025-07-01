namespace ManejoPresupuesto.Servicios
{
    public interface IServiciosUsuarios
    {
        int ObtenerUsuarioId();
    }

    public class ServicioUsuarios: IServiciosUsuarios
    {
        public int ObtenerUsuarioId()
        {
            return 1;
        }
    }
}
