using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Data.Interfaces;
using Acais.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Acais.API.Data.Repositories
{
    public class PedidoRepository : BaseRepository, IPedidoRepository
    {
        private readonly IDataContext _context;
        public PedidoRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pedido> GetPedido(Guid id)
        {
            return await _context.Pedidos.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Pedido>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task<Pedido> RegisterPedido(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<Pedido> UpdatePedido(Guid pedidoId, int tempoPreparo, decimal valor)
        {
            var pedido = await _context.Pedidos.SingleOrDefaultAsync(p => p.Id == pedidoId);
            pedido.TempoPreparo += tempoPreparo;
            pedido.ValorTotal += valor;

            await _context.SaveChangesAsync();
            
            return pedido;
        }
    }
}