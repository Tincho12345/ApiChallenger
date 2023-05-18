using System.ComponentModel.DataAnnotations;

namespace WebContactos.Modelos
{
    public class Contacto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Display(Name = "Seleccione Imágen")]
        [DataType(DataType.ImageUrl)]
        public string UrlImagen { get; set; }

        [Display(Name = "Nombre de la Empresa")]
        [Required]
        public string Empresa { get; set; }

        [Required]
        [Display(Name = "Correo Electrónico")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Formato de Correo Incorrecto")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Nacimiento")]
        public DateTime Nacimiento { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Formato Incorrecto")]
        [Display(Name = "Número Telefónico")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Lugar de Trabajo")]
        public string WorkPersonal { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
    }
}
