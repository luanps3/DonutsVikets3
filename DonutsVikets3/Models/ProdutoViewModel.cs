using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DonutsVikets.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Descricao { get; set; }

        [Required]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        public string? ImagemUrl { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        // Upload
        public IFormFile? ImageFile { get; set; }
    }
}
