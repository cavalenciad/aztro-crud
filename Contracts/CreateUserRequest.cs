using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Contracts
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 20 caracteres")]
        public required string? Name { get; set; }
        
        [Required(ErrorMessage = "El email es requerido")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "El email debe tener al menos 8 caracteres")]
        public required string? Email { get; set; }
        
        [Range(18, 80, ErrorMessage = "La edad debe estar entre 18 y 80 años")]
        public required int Age { get; set; }
    }
}