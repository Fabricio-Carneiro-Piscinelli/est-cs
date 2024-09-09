using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIPOC.Entities
{
    public class Rango
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Nome { get; set; }

        public ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();

        public Rango()
        {
        }

        public Rango(int id, string name)
        {
            Id = id;
            Nome = name;
        }
    }
}
