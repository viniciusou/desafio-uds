using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Models;

namespace Acais.API.Data.Interfaces
{
    public interface IPersonalizacaoRepository : IBaseRepository
    {
        Task<IEnumerable<Personalizacao>> GetPersonalizacoes();
        Task<Personalizacao> GetPersonalizacao(Guid id);
        Task<Personalizacao> GetPersonalizacao(string nome);
        Task<bool> PersonalizacaoExists(string nome);
    }
}