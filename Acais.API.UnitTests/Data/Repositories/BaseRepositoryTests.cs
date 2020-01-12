using System;
using System.Threading.Tasks;
using Acais.API.Data.Interfaces;
using Acais.API.Data.Repositories;
using Acais.API.Models;
using Moq;
using NUnit.Framework;

namespace Acais.API.UnitTests.Data.Repositories
{
    [TestFixture]
    public class BaseRepositoryTests
    {
        private Mock<IDataContext> _context;
        private BaseRepository _baseRepository;
        private Pedido _objetoPedido;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IDataContext>();
            _baseRepository = new BaseRepository(_context.Object);
            _objetoPedido = new Pedido{Id = new Guid()};
        }

        [Test]
        public void Add_AoSerChamado_AdicionaObjetoAFonteDados()
        {
            _baseRepository.Add(_objetoPedido);

            _context.Verify(c => c.Add(_objetoPedido));
        }

        [Test]
        public void Delete_AoSerChamado_RemoveObjetoDaFonteDados()
        {
            _baseRepository.Delete(_objetoPedido);

            _context.Verify(c => c.Remove(_objetoPedido));
        }

        [Test]
        public async Task SaveAll_AoSerChamado_SalvaAlteracoesAFonteDados()
        {
            await _baseRepository.SaveAll();

            _context.Verify(c => c.SaveChangesAsync());
        }
    }
}