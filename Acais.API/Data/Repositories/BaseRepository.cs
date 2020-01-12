using System.Threading.Tasks;
using Acais.API.Data.Interfaces;

namespace Acais.API.Data.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IDataContext _context;
        public BaseRepository(IDataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}