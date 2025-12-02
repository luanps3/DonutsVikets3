using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DonutsVikets.Models;

namespace DonutsVikets3.Data
{
    public class DonutsVikets3Context : DbContext
    {
        public DonutsVikets3Context (DbContextOptions<DonutsVikets3Context> options)
            : base(options)
        {
        }

        public DbSet<DonutsVikets.Models.Produto> Produto { get; set; } = default!;
        public DbSet<DonutsVikets.Models.Categoria> Categoria { get; set; } = default!;
        public DbSet<DonutsVikets.Models.Pedido> Pedido { get; set; } = default!;
        public DbSet<DonutsVikets.Models.ItemPedido> ItemPedido { get; set; } = default!;
        public DbSet<DonutsVikets.Models.Usuario> Usuario { get; set; } = default!;
        public DbSet<DonutsVikets.Models.TipoUsuario> TipoUsuario { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed TipoUsuario
            modelBuilder.Entity<TipoUsuario>().HasData(
                new TipoUsuario { Id = 1, Nome = "Administrador" },
                new TipoUsuario { Id = 2, Nome = "Cliente" }
            );

            // Seed Usuario (senha hash somente para exemplo)
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nome = "Admin", Email = "admin@donuts.local", SenhaHash = "admin", TipoUsuarioId = 1 },
                new Usuario { Id = 2, Nome = "Cliente Padrão", Email = "cliente@donuts.local", SenhaHash = "cliente", TipoUsuarioId = 2 }
            );

            // Seed Categoria
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Clássicos", Descricao = "Donuts tradicionais" },
                new Categoria { Id = 2, Nome = "Recheados", Descricao = "Donuts com vários recheios" },
                new Categoria { Id = 3, Nome = "Especiais", Descricao = "Sabores especiais e sazonais" }
            );

            // Seed Produto
            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Glazed", Descricao = "Donut coberto com glacê", Preco = 5.50m, ImagemUrl = "/img/glazed.jpg", CategoriaId = 1 },
                new Produto { Id = 2, Nome = "Chocolate", Descricao = "Donut com cobertura de chocolate", Preco = 6.00m, ImagemUrl = "/img/chocolate.jpg", CategoriaId = 1 },
                new Produto { Id = 3, Nome = "Recheado de Creme", Descricao = "Donut recheado com creme", Preco = 7.50m, ImagemUrl = "/img/creme.jpg", CategoriaId = 2 },
                new Produto { Id = 4, Nome = "Recheado de Doce de Leite", Descricao = "Donut recheado com doce de leite", Preco = 7.90m, ImagemUrl = "/img/doceleite.jpg", CategoriaId = 2 },
                new Produto { Id = 5, Nome = "Red Velvet", Descricao = "Donut especial red velvet", Preco = 8.90m, ImagemUrl = "/img/redvelvet.jpg", CategoriaId = 3 }
            );

            // Seed Pedido
            modelBuilder.Entity<Pedido>().HasData(
                new Pedido { Id = 1, DataPedido = DateTime.UtcNow.Date, Status = "Pendente", UsuarioId = 2 }
            );

            // Seed ItemPedido
            modelBuilder.Entity<ItemPedido>().HasData(
                new ItemPedido { Id = 1, PedidoId = 1, ProdutoId = 1, Quantidade = 2, PrecoUnitario = 5.50m },
                new ItemPedido { Id = 2, PedidoId = 1, ProdutoId = 3, Quantidade = 1, PrecoUnitario = 7.50m }
            );
        }
    }
}
