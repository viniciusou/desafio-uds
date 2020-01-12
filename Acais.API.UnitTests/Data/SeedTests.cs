using System;
using System.Collections.Generic;
using System.Linq;
using Acais.API.Data;
using Acais.API.Data.Interfaces;
using Acais.API.Models;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace Acais.API.UnitTests.Data
{
    [TestFixture]
    public class SeedTests
    {
        private Mock<IDataContext> _context;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IDataContext>();
        }

        [Test]
        public void Seed_FonteDadosVazia_TamanhosAdicionadosAFonteDados()
        {
            var tamanhos = new List<Tamanho>();
            var mockTamanhos = tamanhos.AsQueryable().BuildMockDbSet();
            _context.Setup(t => t.Tamanhos).Returns(mockTamanhos.Object);

            var sabores = new List<Sabor>();
            var mockSabores = sabores.AsQueryable().BuildMockDbSet();
            _context.Setup(t => t.Sabores).Returns(mockSabores.Object);

            Seed.SeedData(_context.Object);

            _context.Verify(c => c.Tamanhos.AddRange(It.IsAny<List<Tamanho>>()));
        }

        [Test]
        public void Seed_FonteDadosVazia_SaboresAdicionadosAFonteDados()
        {
            var tamanhos = new List<Tamanho>();
            var mockTamanhos = tamanhos.AsQueryable().BuildMockDbSet();
            _context.Setup(t => t.Tamanhos).Returns(mockTamanhos.Object);

            var sabores = new List<Sabor>();
            var mockSabores = sabores.AsQueryable().BuildMockDbSet();
            _context.Setup(t => t.Sabores).Returns(mockSabores.Object);

            Seed.SeedData(_context.Object);

            _context.Verify(c => c.Sabores.AddRange(It.IsAny<List<Sabor>>()));
        }

        [Test]
        public void Seed_FonteDadosVazia_DadosAdicionadosSalvosAFonteDados()
        {
            var tamanhos = new List<Tamanho>();
            var mockTamanhos = tamanhos.AsQueryable().BuildMockDbSet();
            _context.Setup(t => t.Tamanhos).Returns(mockTamanhos.Object);

            var sabores = new List<Sabor>();
            var mockSabores = sabores.AsQueryable().BuildMockDbSet();
            _context.Setup(t => t.Sabores).Returns(mockSabores.Object);

            Seed.SeedData(_context.Object);

            _context.Verify(c => c.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void Seed_FonteDadosNaoVazia_DadosNaoAdicionadosAFonteDados()
        {
            var tamanhos = new List<Tamanho>{ new Tamanho {Id = new Guid()}};
            var mockTamanhos = tamanhos.AsQueryable().BuildMockDbSet();
            _context.Setup(t => t.Tamanhos).Returns(mockTamanhos.Object);

            var sabores = new List<Sabor> { new Sabor {Id = new Guid() }};
            var mockSabores = sabores.AsQueryable().BuildMockDbSet();
            _context.Setup(t => t.Sabores).Returns(mockSabores.Object);

            Seed.SeedData(_context.Object);

            _context.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}