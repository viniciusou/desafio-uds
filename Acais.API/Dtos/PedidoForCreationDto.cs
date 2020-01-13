using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Acais.API.Models;

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