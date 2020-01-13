using System;

namespace Acais.API.Dtos
{
    public class PersonalizacaoToReturnDto
    {
        public Guid Id { get; set; }
        public string Produto { get; set; }
        public int TempoPreparo { get; set; }
        public decimal Valor { get; set; }
    }
}