using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonutsVikets.Models
{
    public class Produto
    {
        public Produto()
        {
            ItensPedido = new List<ItemPedido>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Nome { get; set; }

        [StringLength(255)]
        public string Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [StringLength(200)]
        public string ImagemUrl { get; set; }

        // FK
        [Required]
        [Display(Name = "Categoria")] // Adiciona nome mais amigável para CategoriaId
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; }
    }
}
