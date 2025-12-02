using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonutsVikets.Models
{
    public class Pedido
    {
        public Pedido()
        {
            Itens = new List<ItemPedido>();
        }

        public int Id { get; set; }

        [Required]
        public DateTime DataPedido { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Ex.: "Pendente", "Pago", "Cancelado"

        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<ItemPedido> Itens { get; set; }
    }
}
