using System.Threading.Tasks;
using Acais.API.Data.Interfaces;
using Acais.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Acais.API.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Sabor> Sabores { get; set; }
        public DbSet<Tamanho> Tamanhos { get; set; }
        public DbSet<Personalizacao> Personalizacoes { get; set; }
        public DbSet<PedidoPersonalizacao> PedidoPersonalizacoes { get; set; }

        public new void Add<T>(T entity)
        {
            base.Add(entity);
        }

        public new void Remove<T>(T entity)
        {
            base.Remove(entity);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}