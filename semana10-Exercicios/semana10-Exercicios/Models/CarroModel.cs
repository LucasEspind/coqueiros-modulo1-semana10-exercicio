using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace semana10_Exercicios.Models
{
    [Table("Carro")]
    public class CarroModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Nome { get; set; }
        [Column("Data de Locacao")]
        public DateTime DataLocacao { get; set; }

        [ForeignKey("Marcas")]
        public int IdMarca { get; set; }
        public MarcaModel Marcas { get; set; }

    }
}
