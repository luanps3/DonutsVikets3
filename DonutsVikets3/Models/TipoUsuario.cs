using System.ComponentModel.DataAnnotations;

namespace DonutsVikets.Models
{
    public class TipoUsuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty; // Administrador, Cliente
    }
}
