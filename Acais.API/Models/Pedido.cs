using System;

namespace Acais.API.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public virtual Tamanho Tamanho { get; set; }
        public Guid TamanhoId { get; set; }
        public virtual Sabor Sabor { get; set; }
        public Guid SaborId { get; set; }
        public int Tempo { get; set; }
        public decimal Valor { get; set; }
    }
}