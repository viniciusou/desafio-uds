using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Models;

namespace Acais.API.Data.Interfaces
{
    public interface ISaborRepository : IBaseRepository
    {
        Task<IEnumerable<Sabor>> GetSabores();
        Task<Sabor> GetSabor(Guid id);
        Task<Sabor> GetSabor(string nome);
        Task<bool> SaborExists(string nome);
    }
}