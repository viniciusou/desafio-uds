using System;

namespace Acais.API.Models
{
    public class Tamanho
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int TempoPreparo { get; set; }
        public decimal Valor { get; set; }
    }
}