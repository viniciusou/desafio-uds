using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Models;

namespace Acais.API.Data.Interfaces
{
    public interface IPedidoPersonalizacaoRepository : IBaseRepository
    {
        Task<PedidoPersonalizacao> GetPedidoPersonalizacao(Guid id);
        Task<IEnumerable<PedidoPersonalizacao>> GetPedidoPersonalizacoes();
        Task<PedidoPersonalizacao> RegisterPedidoPersonalizacao(PedidoPersonalizacao pedidoPersonalizacao);
    }
}