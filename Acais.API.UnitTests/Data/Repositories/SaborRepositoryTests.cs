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
    public class SaborRepositoryTests
    {
        private Mock<IDataContext> _context;
        private SaborRepository _saborRepository;
        private Sabor _sabor;
        private List<Sabor> _sabores;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IDataContext>();
            _saborRepository = new SaborRepository(_context.Object);

            _sabor = new Sabor { Id = new Guid(), Nome = "Teste" };
            _sabores = new List<Sabor> { _sabor };
            var mockSabores = _sabores.AsQueryable().BuildMockDbSet();
            _context.Setup(c => c.Sabores).Returns(mockSabores.Object);
        }

        [Test]
        public async Task GetSabores_AoSerChamado_RetornaSaboresDaFonteDados()
        {
            var result = await _saborRepository.GetSabores();

            Assert.That(result, Is.EqualTo(_sabores));
        }

        [Test]
        public async Task GetSabor_AoSerChamadoViaNome_RetornaSaborDaFonteDados()
        {
            var result = await _saborRepository.GetSabor(_sabor.Nome);

            Assert.That(result, Is.EqualTo(_sabor));
        }

        [Test]
        public async Task GetSabor_AoSerChamadoViaId_RetornaSaborDaFonteDados()
        {
            var result = await _saborRepository.GetSabor(_sabor.Id);

            Assert.That(result, Is.EqualTo(_sabor));
        }

        [Test]
        public async Task SaborExists_SaborNaoExisteNaFonteDados_RetornaFalse()
        {
            var newSabor = new Sabor { Id = new Guid(), Nome = "NovoTeste" };

            var result = await _saborRepository.SaborExists(newSabor.Nome);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task SaborExists_SaborJaExisteNaFonteDados_RetornaTrue()
        {
            var result = await _saborRepository.SaborExists(_sabor.Nome);

            Assert.That(result, Is.True);
        }
    }
}