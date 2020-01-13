using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acais.API.Models
{
    public class Personalizacao
    {
        public Guid Id { get; set; }
        [Required]
        public string Produto { get; set; }
        public int TempoPreparo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
        public virtual ICollection<PedidoPersonalizacao> PedidoPersonalizacoes { get; set; }
    }
}