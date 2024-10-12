
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.DTO.Account
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El usuario de usuario es obligatorio.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}