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
    public class PedidoPersonalizacaoRepositoryTests
    {
        private Mock<IDataContext> _context;
        private PedidoPersonalizacaoRepository _pedidoPersonalizacaoRepository;
        private Guid _pedidoPersonalizacaoId;
        private PedidoPersonalizacao _pedidoPersonalizacao;
        private List<PedidoPersonalizacao> _pedidoPersonalizacoes;
        private PedidoPersonalizacao _newPedidoPersonalizacao;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IDataContext>();
            _pedidoPersonalizacaoRepository = new PedidoPersonalizacaoRepository(_context.Object);

            _pedidoPersonalizacaoId = new Guid();
            _pedidoPersonalizacao = new PedidoPersonalizacao { Id = _pedidoPersonalizacaoId };
            _pedidoPersonalizacoes = new List<PedidoPersonalizacao> { _pedidoPersonalizacao };
            var mockPedidoPersonalizacoes = _pedidoPersonalizacoes.AsQueryable().BuildMockDbSet();
            _context.Setup(c => c.PedidoPersonalizacoes).Returns(mockPedidoPersonalizacoes.Object);

            _newPedidoPersonalizacao = new PedidoPersonalizacao {Id = new Guid()};
        }

        [Test]
        public async Task GetPedidoPersonalizacao_AoSerChamado_ObtemPedidoPersonalizacaoDaFonteDados()
        {
            var result = await _pedidoPersonalizacaoRepository.GetPedidoPersonalizacao(_pedidoPersonalizacaoId);

            Assert.That(result, Is.EqualTo(_pedidoPersonalizacao));
        }

        [Test]
        public async Task GetPedidoPersonalizacoes_AoSerChamado_ObtemPedidoPersonalizacoesDaFonteDados()
        {
            var result = await _pedidoPersonalizacaoRepository.GetPedidoPersonalizacoes();

            Assert.That(result, Is.EqualTo(_pedidoPersonalizacoes));
        }

        [Test]
        public async Task RegisterPedidoPersonalizacao_AoSerChamado_AdicionaPedidoPersonalizacaoAFonteDados()
        {
            await _pedidoPersonalizacaoRepository.RegisterPedidoPersonalizacao(_newPedidoPersonalizacao);

            _context.Verify(c => c.PedidoPersonalizacoes.AddAsync(_newPedidoPersonalizacao, CancellationToken.None));
        }

        [Test]
        public async Task RegisterPedidoPersonalizacao_AoSerChamado_RetornaPedidoPersonalizacaoAdicionadoAFonteDados()
        {
            var result = await _pedidoPersonalizacaoRepository.RegisterPedidoPersonalizacao(_newPedidoPersonalizacao);

            Assert.That(result, Is.EqualTo(_newPedidoPersonalizacao));
        }
    }
}