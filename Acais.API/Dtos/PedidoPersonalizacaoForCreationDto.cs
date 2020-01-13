using System;

namespace Acais.API.Dtos
{
    public class PedidoPersonalizacaoForCreationDto
    {
        public Guid PedidoId { get; set; }
        public Guid PersonalizacaoId { get; set; }
    }
}