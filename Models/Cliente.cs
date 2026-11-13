using System.ComponentModel.DataAnnotations;

namespace CrudClientes.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula/RUC es obligatoria")]
        [StringLength(20)]
        [Display(Name = "Cédula / RUC")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        [StringLength(100)]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [StringLength(150)]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [StringLength(60)]
        [Display(Name = "Ciudad")]
        public string? Ciudad { get; set; }

        [StringLength(60)]
        [Display(Name = "País")]
        public string? Pais { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        [StringLength(200)]
        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }
    }
}
