using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Data.Interfaces;
using Acais.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Acais.API.Data.Repositories
{
    public class PedidoPersonalizacaoRepository : BaseRepository, IPedidoPersonalizacaoRepository
    {
        private readonly IDataContext _context;
        public PedidoPersonalizacaoRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PedidoPersonalizacao> GetPedidoPersonalizacao(Guid id)
        {
            return await _context.PedidoPersonalizacoes.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PedidoPersonalizacao>> GetPedidoPersonalizacoes()
        {
            return await _context.PedidoPersonalizacoes.ToListAsync();
        }

        public async Task<PedidoPersonalizacao> RegisterPedidoPersonalizacao(PedidoPersonalizacao pedidoPersonalizacao)
        {
            await _context.PedidoPersonalizacoes.AddAsync(pedidoPersonalizacao);
            await _context.SaveChangesAsync();

            return pedidoPersonalizacao;
        }
    }
}