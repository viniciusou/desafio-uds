using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Data.Interfaces;
using Acais.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Acais.API.Data.Repositories
{
    public class TamanhoRepository : BaseRepository, ITamanhoRepository
    {
        private readonly IDataContext _context;
        public TamanhoRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tamanho>> GetTamanhos()
        {
            return await _context.Tamanhos.ToListAsync();
        }

        public async Task<Tamanho> GetTamanho(Guid id)
        {
            return await _context.Tamanhos.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tamanho> GetTamanho(string nome)
        {
            return await _context.Tamanhos.SingleOrDefaultAsync(t => t.Nome == nome);
        }

        public async Task<bool> TamanhoExists(string nome)
        {
            if (await _context.Tamanhos.AnyAsync(t => t.Nome == nome))
                return true;

            return false;
        }

    }
}