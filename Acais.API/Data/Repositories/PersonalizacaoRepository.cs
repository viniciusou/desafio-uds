using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Data.Interfaces;
using Acais.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Acais.API.Data.Repositories
{
    public class PersonalizacaoRepository : BaseRepository, IPersonalizacaoRepository
    {
        private readonly IDataContext _context;
        public PersonalizacaoRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Personalizacao>> GetPersonalizacoes()
        {
            return await _context.Personalizacoes.ToListAsync();
        }

        public async Task<Personalizacao> GetPersonalizacao(Guid id)
        {
            return await _context.Personalizacoes.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Personalizacao> GetPersonalizacao(string nomeProduto)
        {
            return await _context.Personalizacoes.SingleOrDefaultAsync(p => p.Produto == nomeProduto);
        }

        public async Task<bool> PersonalizacaoExists(string nomeProduto)
        {
            if (await _context.Personalizacoes.AnyAsync(p => p.Produto == nomeProduto))
                return true;

            return false;
        }
    }
}