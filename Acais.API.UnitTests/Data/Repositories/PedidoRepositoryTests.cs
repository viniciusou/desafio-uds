using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acais.API.Data.Interfaces;
using Acais.API.Data.Repositories;
using Acais.API.Models;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace Acais.API.UnitTests.Data.Repositories
{
    [TestFixture]
    public class PedidoRepositoryTests
    {
        private Mock<IDataContext> _context;
        private PedidoRepository _pedidoRepository;
        private Guid _pedidoId;
        private Pedido _pedido;
        private List<Pedido> _pedidos; 

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IDataContext>();
            _pedidoRepository = new PedidoRepository(_context.Object);

            _pedidoId = new Guid();
            _pedido = new Pedido { Id = _pedidoId };
            _pedidos = new List<Pedido> { _pedido };
            var mockPedidos = _pedidos.AsQueryable().BuildMockDbSet();
            _context.Setup(c => c.Pedidos).Returns(mockPedidos.Object);
        }

        [Test]
        public async Task GetPedido_AoSerChamado_ObtemPedidoDaFonteDados()
        {
            var result = await _pedidoRepository.GetPedido(_pedidoId);

            Assert.That(result, Is.EqualTo(_pedido));
        }

        [Test]
        public async Task GetPedidos_AoSerChamado_ObtemPedidosDaFonteDados()
        {
            var result = await _pedidoRepository.GetPedidos();

            Assert.That(result, Is.EqualTo(_pedidos));
        }

        [Test]
        public async Task RegisterPedido_AoSerChamado_AdicionaPedidoAFonteDados()
        {
            var newPedido = new Pedido { Id = new Guid() };

            await _pedidoRepository.RegisterPedido(newPedido);

            _context.Verify(c => c.Pedidos.AddAsync(newPedido, CancellationToken.None));
        }

        [Test]
        public async Task RegisterPedido_AoSerChamado_RetornaPedidoAdicionadoAFonteDados()
        {
            var newPedido = new Pedido { Id = new Guid() };

            var result = await _pedidoRepository.RegisterPedido(newPedido);

            Assert.That(result, Is.EqualTo(newPedido));
        }
    }
}