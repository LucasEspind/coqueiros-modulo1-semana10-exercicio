using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace semana10_Exercicios.Models
{
    [Table("Marca")]
    public class MarcaModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Nome { get; set; }
    }
}
