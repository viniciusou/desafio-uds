using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Models;

namespace Acais.API.Data.Interfaces
{
    public interface IPedidoRepository : IBaseRepository
    {
        Task<IEnumerable<Pedido>> GetPedidos();

        Task<Pedido> GetPedido(Guid id);

        Task<Pedido> RegisterPedido(Pedido pedido);
    }
}