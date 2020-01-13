using System;

namespace Acais.API.Dtos
{
    public class PedidoPersonalizacaoToReturnDto
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid PersonalizacaoId { get; set; }
    }
}