using System;
using System.Collections.Generic;
using Acais.API.Models;

namespace Acais.API.Dtos
{
    public class PedidoToReturnDto
    {
        public Guid Id { get; set; }
        public string Tamanho { get; set; }
        public string Sabor { get; set; }
        public int TempoPreparo { get; set; }
        public decimal ValorTotal { get; set; }
        public ICollection<string> Personalizacao { get; set; }
    }
}