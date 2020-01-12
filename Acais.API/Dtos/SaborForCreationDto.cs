using System.ComponentModel.DataAnnotations;

namespace Acais.API.Dtos
{
    public class SaborForCreationDto
    {
        [Required]
        public string Nome { get; set; }
        [Range(0, int.MaxValue)]
        public int TempoPreparo { get; set; }
    }
}