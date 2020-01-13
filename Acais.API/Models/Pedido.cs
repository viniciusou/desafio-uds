using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acais.API.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public virtual Tamanho Tamanho { get; set; }
        public Guid TamanhoId { get; set; }
        public virtual Sabor Sabor { get; set; }
        public Guid SaborId { get; set; }
        public int TempoPreparo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorTotal { get; set; }
        public virtual ICollection<PedidoPersonalizacao> PedidoPersonalizacoes { get; set; }
    }
}