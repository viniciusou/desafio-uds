using System;
using System.ComponentModel.DataAnnotations;

namespace Acais.API.Models
{
    public class Sabor
    {
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public int TempoPreparo { get; set; }
    }
}