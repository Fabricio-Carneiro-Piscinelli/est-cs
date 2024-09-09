using Microsoft.EntityFrameworkCore;
using APIPOC.Entities;

namespace APIPOC.DbContexts
{
    public class RangoDbContext : DbContext
    {
        public RangoDbContext(DbContextOptions<RangoDbContext> options) : base(options)
        {
        }

        public DbSet<Rango> Rangos { get; set; } = null!;
        public DbSet<Ingrediente> Ingredientes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar dados iniciais para Ingredientes
            modelBuilder.Entity<Ingrediente>().HasData(
                new Ingrediente { Id = 1, Nome = "Carne de Vaca" },
                new Ingrediente { Id = 2, Nome = "Cebola" },
                new Ingrediente { Id = 3, Nome = "Cerveja Escura" },
                new Ingrediente { Id = 4, Nome = "Fatia de Pão Integral" },
                new Ingrediente { Id = 5, Nome = "Mostarda" },
                new Ingrediente { Id = 6, Nome = "Chicória" },
                new Ingrediente { Id = 7, Nome = "Maionese" },
                new Ingrediente { Id = 8, Nome = "Vários Temperos" },
                new Ingrediente { Id = 9, Nome = "Mexilhões" },
                new Ingrediente { Id = 10, Nome = "Aipo" },
                new Ingrediente { Id = 11, Nome = "Batatas Fritas" },
                new Ingrediente { Id = 12, Nome = "Tomate" },
                new Ingrediente { Id = 13, Nome = "Extrato de Tomate" },
                new Ingrediente { Id = 14, Nome = "Folha de Louro" },
                new Ingrediente { Id = 15, Nome = "Cenoura" },
                new Ingrediente { Id = 16, Nome = "Alho" },
                new Ingrediente { Id = 17, Nome = "Vinho Tinto" },
                new Ingrediente { Id = 18, Nome = "Leite de Coco" },
                new Ingrediente { Id = 19, Nome = "Gengibre" },
                new Ingrediente { Id = 20, Nome = "Pimenta Malagueta" },
                new Ingrediente { Id = 21, Nome = "Tamarindo" },
                new Ingrediente { Id = 22, Nome = "Peixe Firme" },
                new Ingrediente { Id = 23, Nome = "Pasta de Gengibre e Alho" },
                new Ingrediente { Id = 24, Nome = "Garam Masala" });

            // Configurar dados iniciais para Rangos
            modelBuilder.Entity<Rango>().HasData(
                new Rango { Id = 1, Nome = "Ensopado Flamengo de Carne de Vaca com Chicória" },
                new Rango { Id = 2, Nome = "Mexilhões com Batatas Fritas" },
                new Rango { Id = 3, Nome = "Ragu alla Bolognese" },
                new Rango { Id = 4, Nome = "Rendang" },
                new Rango { Id = 5, Nome = "Masala de Peixe" });

            // Configurar relacionamento entre Rangos e Ingredientes
            modelBuilder.Entity<Rango>()
                .HasMany(r => r.Ingredientes)
                .WithMany(i => i.Rangos)
                .UsingEntity(j => j
                    .HasData(
                        new { RangosId = 1, IngredientesId = 1 },
                        new { RangosId = 1, IngredientesId = 2 },
                        new { RangosId = 1, IngredientesId = 3 },
                        new { RangosId = 1, IngredientesId = 4 },
                        new { RangosId = 1, IngredientesId = 5 },
                        new { RangosId = 1, IngredientesId = 6 },
                        new { RangosId = 1, IngredientesId = 7 },
                        new { RangosId = 1, IngredientesId = 8 },
                        new { RangosId = 1, IngredientesId = 14 },
                        new { RangosId = 2, IngredientesId = 9 },
                        new { RangosId = 2, IngredientesId = 19 },
                        new { RangosId = 2, IngredientesId = 11 },
                        new { RangosId = 2, IngredientesId = 12 },
                        new { RangosId = 2, IngredientesId = 13 },
                        new { RangosId = 2, IngredientesId = 2 },
                        new { RangosId = 2, IngredientesId = 21 },
                        new { RangosId = 2, IngredientesId = 8 },
                        new { RangosId = 3, IngredientesId = 1 },
                        new { RangosId = 3, IngredientesId = 12 },
                        new { RangosId = 3, IngredientesId = 17 },
                        new { RangosId = 3, IngredientesId = 14 },
                        new { RangosId = 3, IngredientesId = 2 },
                        new { RangosId = 3, IngredientesId = 16 },
                        new { RangosId = 3, IngredientesId = 23 },
                        new { RangosId = 3, IngredientesId = 8 },
                        new { RangosId = 4, IngredientesId = 1 },
                        new { RangosId = 4, IngredientesId = 18 },
                        new { RangosId = 4, IngredientesId = 16 },
                        new { RangosId = 4, IngredientesId = 20 },
                        new { RangosId = 4, IngredientesId = 22 },
                        new { RangosId = 4, IngredientesId = 2 },
                        new { RangosId = 4, IngredientesId = 21 },
                        new { RangosId = 4, IngredientesId = 8 },
                        new { RangosId = 5, IngredientesId = 24 },
                        new { RangosId = 5, IngredientesId = 10 },
                        new { RangosId = 5, IngredientesId = 23 },
                        new { RangosId = 5, IngredientesId = 2 },
                        new { RangosId = 5, IngredientesId = 12 },
                        new { RangosId = 5, IngredientesId = 18 },
                        new { RangosId = 5, IngredientesId = 14 },
                        new { RangosId = 5, IngredientesId = 20 },
                        new { RangosId = 5, IngredientesId = 13 }));

            base.OnModelCreating(modelBuilder);
        }
    }
}
