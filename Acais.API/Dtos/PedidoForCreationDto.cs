using System.ComponentModel.DataAnnotations;

namespace Acais.API.Dtos
{
    public class PedidoForCreationDto
    {
        [Required]
        public string Tamanho { get; set; }
        [Required]
        public string Sabor { get; set; }
    }
}