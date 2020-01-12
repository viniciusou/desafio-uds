using System;

namespace Acais.API.Dtos
{
    public class PedidoToReturnDto
    {
        public Guid Id { get; set; }
        public string Tamanho { get; set; }
        public string Sabor { get; set; }
        public int Tempo { get; set; }
        public decimal Valor { get; set; }
    }
}