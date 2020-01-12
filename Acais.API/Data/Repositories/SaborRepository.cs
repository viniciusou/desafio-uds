using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acais.API.Data.Interfaces;
using Acais.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Acais.API.Data.Repositories
{
    public class SaborRepository : BaseRepository, ISaborRepository
    {
        private readonly IDataContext _context;
        public SaborRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sabor>> GetSabores()
        {
            return await _context.Sabores.ToListAsync();
        }
        public async Task<Sabor> GetSabor(Guid id)
        {
            return await _context.Sabores.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sabor> GetSabor(string nome)
        {
            return await _context.Sabores.SingleOrDefaultAsync(s => s.Nome == nome);
        }

        public async Task<bool> SaborExists(string nome)
        {
            if (await _context.Sabores.AnyAsync(s => s.Nome == nome))
                return true;

            return false;
        }

    }
}