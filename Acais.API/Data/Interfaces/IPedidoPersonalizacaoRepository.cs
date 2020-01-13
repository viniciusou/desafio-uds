using System;
using System.Threading.Tasks;
using Acais.API.Models;

namespace Acais.API.Data.Interfaces
{
    public interface IPedidoPersonalizacaoRepository : IBaseRepository
    {
        Task<PedidoPersonalizacao> GetPedidoPersonalizacao(Guid id);
        Task<PedidoPersonalizacao> RegisterPedidoPersonalizacao(PedidoPersonalizacao pedidoPersonalizacao);
    }
}