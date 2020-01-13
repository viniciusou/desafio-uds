using System;

namespace Acais.API.Models
{
    public class PedidoPersonalizacao
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid PersonalizacaoId { get; set; }
        //public string PersonalizacaoProduto { get; set; }

        public virtual Personalizacao Personalizacao { get; set; }
    }
}