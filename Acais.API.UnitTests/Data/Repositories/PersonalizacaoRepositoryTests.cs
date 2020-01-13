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
    public class PersonalizacaoRepositoryTests
    {
        private Mock<IDataContext> _context;
        private PersonalizacaoRepository _personalizacaoRepository;
        private Personalizacao _personalizacao;
        private List<Personalizacao> _personalizacoes;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IDataContext>();
            _personalizacaoRepository = new PersonalizacaoRepository(_context.Object);

            _personalizacao = new Personalizacao { Id = new Guid(), Produto = "Teste" };
            _personalizacoes = new List<Personalizacao> { _personalizacao };
            var mockPersonalizacoes = _personalizacoes.AsQueryable().BuildMockDbSet();
            _context.Setup(c => c.Personalizacoes).Returns(mockPersonalizacoes.Object);
        }

        [Test]
        public async Task GetPersonalizacoes_AoSerChamado_RetornaPersonalizacoesDaFonteDados()
        {
            var result = await _personalizacaoRepository.GetPersonalizacoes();

            Assert.That(result, Is.EqualTo(_personalizacoes));
        }

        [Test]
        public async Task GetPersonalizacao_AoSerChamadoViaNome_RetornaPersonalizacaoDaFonteDados()
        {
            var result = await _personalizacaoRepository.GetPersonalizacao(_personalizacao.Produto);

            Assert.That(result, Is.EqualTo(_personalizacao));
        }

        [Test]
        public async Task GetPersonalizacao_AoSerChamadoViaId_RetornaPersonalizacaoDaFonteDados()
        {
            var result = await _personalizacaoRepository.GetPersonalizacao(_personalizacao.Id);

            Assert.That(result, Is.EqualTo(_personalizacao));
        }

        [Test]
        public async Task PersonalizacaoExists_PersonalizacaoNaoExisteNaFonteDados_RetornaFalse()
        {
            var newPersonalizacao = new Personalizacao { Id = new Guid(), Produto = "NovoTeste" };

            var result = await _personalizacaoRepository.PersonalizacaoExists(newPersonalizacao.Produto);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task PersonalizacaoExists_PersonalizacaoJaExisteNaFonteDados_RetornaTrue()
        {
            var result = await _personalizacaoRepository.PersonalizacaoExists(_personalizacao.Produto);

            Assert.That(result, Is.True);
        }
    }
}