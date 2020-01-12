using System;

namespace Acais.API.Models
{
    public class Sabor
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int TempoPreparo { get; set; }
    }
}