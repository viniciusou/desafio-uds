using System.ComponentModel.DataAnnotations;

namespace Acais.API.Dtos
{
    public class TamanhoForCreationDto
    {
        [Required]
        public string Nome { get; set; }
        [Range(0, int.MaxValue)]
        public int TempoPreparo { get; set; }
        [RegularExpression(@"^\d+.?\d{0,2}$")]
        public decimal Valor { get; set; }
    }
}