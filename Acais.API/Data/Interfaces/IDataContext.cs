using System.Threading.Tasks;
using Acais.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Acais.API.Data.Interfaces
{
    public interface IDataContext
    {
        DbSet<Pedido> Pedidos { get; set; }
        DbSet<Sabor> Sabores { get; set; }
        DbSet<Tamanho> Tamanhos { get; set; }
        DbSet<Personalizacao> Personalizacoes { get; set; }
        DbSet<PedidoPersonalizacao> PedidoPersonalizacoes { get; set; }

        void Add<T>(T entity);

        void Remove<T>(T entity);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}