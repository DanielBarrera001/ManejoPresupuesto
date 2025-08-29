using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage ="El campo Email es requerido")]
        [EmailAddress(ErrorMessage ="El campo debe ser un email valido")]
        public string Email {  get; set; }
        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        public string Password { get; set; }
    }
}
