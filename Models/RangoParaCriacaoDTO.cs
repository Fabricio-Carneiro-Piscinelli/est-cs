using System.ComponentModel.DataAnnotations;

namespace APIPOC.Models {
    public class RangoParaCriacaoDTO {
        [Required]
        [StringLength(100 , MinimumLength = 3)]
        public required string Nome {get; set;}   
    }
};
