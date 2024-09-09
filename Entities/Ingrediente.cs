using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIPOC.Entities
{
    public class Ingrediente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Nome { get; set; }

        // Se a coleção Ingredientes se refere a um relacionamento recursivo, você pode mantê-la. Caso contrário, remova-a.
        public ICollection<Rango> Rangos { get; set; } = new List<Rango>();

        public Ingrediente()
        {
        }

        public Ingrediente(int id, string name)
        {
            Id = id;
            Nome = name;
        }
    }
}
