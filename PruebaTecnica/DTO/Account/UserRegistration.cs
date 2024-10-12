

using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.DTO.Account
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre completo no puede tener más de 50 caracteres.")]
        public string FullName { get; set; }
       
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres.")]
        public string Username { get; set; }
       
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(256, ErrorMessage = "El correo electrónico no puede tener más de 256 caracteres.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 50 caracteres.")]
        public string Password { get; set; }
    }
}