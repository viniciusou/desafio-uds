using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Models;

namespace Acais.API.Data.Interfaces
{
    public interface ITamanhoRepository : IBaseRepository
    {
        Task<IEnumerable<Tamanho>> GetTamanhos();
        Task<Tamanho> GetTamanho(Guid id);
        Task<Tamanho> GetTamanho(string nome);
        Task<bool> TamanhoExists(string nome);
    }
}