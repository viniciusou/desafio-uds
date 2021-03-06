using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acais.API.Models
{
    public class Tamanho
    {
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public int TempoPreparo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
    }
}