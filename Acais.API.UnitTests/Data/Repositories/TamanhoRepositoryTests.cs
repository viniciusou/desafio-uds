using System;
using System.Collections.Generic;
using System.Linq;
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
    public class tamanhoRepositoryTests
    {
        private Mock<IDataContext> _context;
        private TamanhoRepository _tamanhoRepository;
        private Tamanho _tamanho;
        private List<Tamanho> _tamanhos;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IDataContext>();
            _tamanhoRepository = new TamanhoRepository(_context.Object);

            _tamanho = new Tamanho { Id = new Guid(), Nome = "Teste" };
            _tamanhos = new List<Tamanho> { _tamanho };
            var mockTamanhos = _tamanhos.AsQueryable().BuildMockDbSet();
            _context.Setup(c => c.Tamanhos).Returns(mockTamanhos.Object);
        }

        [Test]
        public async Task GetTamanhos_AoSerChamado_RetornaTamanhosDaFonteDados()
        {
            var result = await _tamanhoRepository.GetTamanhos();

            Assert.That(result, Is.EqualTo(_tamanhos));
        }

        [Test]
        public async Task GetTamanho_AoSerChamadoViaNome_RetornaTamanhoDaFonteDados()
        {
            var result = await _tamanhoRepository.GetTamanho(_tamanho.Nome);

            Assert.That(result, Is.EqualTo(_tamanho));
        }

        [Test]
        public async Task GetTamanho_AoSerChamadoViaId_RetornaTamanhoDaFonteDados()
        {
            var result = await _tamanhoRepository.GetTamanho(_tamanho.Id);

            Assert.That(result, Is.EqualTo(_tamanho));
        }

        [Test]
        public async Task TamanhoExists_TamanhoNaoExisteNaFonteDados_RetornaFalse()
        {
            var newTamanho = new Tamanho {Id = new Guid(), Nome = "NovoTeste"};

            var result = await _tamanhoRepository.TamanhoExists(newTamanho.Nome);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task TamanhoExists_TamanhoJaExisteNaFonteDados_RetornaTrue()
        {
            var result = await _tamanhoRepository.TamanhoExists(_tamanho.Nome);

            Assert.That(result, Is.True);
        }
    }
}