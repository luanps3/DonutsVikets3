using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonutsVikets.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new List<Produto>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Nome { get; set; }

        [StringLength(255)]
        public string Descricao { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}
