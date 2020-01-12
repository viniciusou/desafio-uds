using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acais.API.Controllers;
using Acais.API.Data.Interfaces;
using Acais.API.Dtos;
using Acais.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Acais.API.UnitTests.Controllers
{
    [TestFixture]
    public class PedidosControllerTests
    {
        private Mock<IPedidoRepository> _pedidoRepository;
        private Mock<ITamanhoRepository> _tamanhoRepository;
        private Mock<ISaborRepository> _saborRepository;
        private Mock<IMapper> _mapper;
        private PedidosController _controller;
        private string _tamanhoNome = "pequeno";
        private string _saborNome = "banana";
        private Guid _tamanhoId = new Guid();
        private Guid _saborId = new Guid();
        private Guid _pedidoId = new Guid();
        private Tamanho _tamanho;
        private Sabor _sabor;

        [SetUp]
        public void SetUp()
        {
            _pedidoRepository = new Mock<IPedidoRepository>();
            _tamanhoRepository = new Mock<ITamanhoRepository>();
            _saborRepository = new Mock<ISaborRepository>();
            _mapper = new Mock<IMapper>();

            _controller = new PedidosController(_pedidoRepository.Object,
                _tamanhoRepository.Object, _saborRepository.Object, _mapper.Object);

            _tamanho = new Tamanho { Id = _tamanhoId, Nome = _tamanhoNome };
            var tamanhos = new List<Tamanho> { _tamanho };

            _tamanhoRepository.Setup(t => t.GetTamanho(_tamanhoId)).Returns(Task.FromResult(_tamanho));
            _tamanhoRepository.Setup(t => t.GetTamanhos()).Returns(Task.FromResult(tamanhos.AsEnumerable()));

            _sabor = new Sabor { Id = _saborId, Nome = _saborNome };
            var sabores = new List<Sabor> { _sabor };

            _saborRepository.Setup(s => s.GetSabor(_saborNome)).Returns(Task.FromResult(_sabor));
            _saborRepository.Setup(s => s.GetSabores()).Returns(Task.FromResult(sabores.AsEnumerable()));

            var pedidos = new List<Pedido>
            {
                new Pedido {Id = _pedidoId},
                new Pedido {Id = new Guid()}
            };

            _pedidoRepository.Setup(p => p.GetPedidos()).Returns(Task.FromResult(pedidos.AsEnumerable()));
        }

        [Test]
        public async Task GetPedidos_AoSerChamado_ObtemPedidosFonteDados()
        {
            await _controller.GetPedidos();

            _pedidoRepository.Verify(p => p.GetPedidos());
        }

        [Test]
        public async Task GetPedidos_AoSerChamado_RetornaOkResponse()
        {
            var result = await _controller.GetPedidos();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetPedido_AoSerChamando_ObtemPedidoFonteDados()
        {
            await _controller.GetPedido(_pedidoId);

            _pedidoRepository.Verify(p => p.GetPedido(_pedidoId));
        }

        [Test]
        public async Task GetPedido_AoSerChamando_RetornaOkResponse()
        {
            var result = await _controller.GetPedido(_pedidoId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreatePedido_TamanhoNaoExisteNaFonteDados_RetornaBadRequestResponse()
        {
            var tamanhoNome = "Teste";

            var nullTamanho = new Tamanho();
            nullTamanho = null;

            _tamanhoRepository.Setup(t => t.GetTamanho(tamanhoNome)).Returns(Task.FromResult(nullTamanho));

            var pedidoForCreationDto = new PedidoForCreationDto { Tamanho = tamanhoNome, Sabor = _saborNome };

            var result = await _controller.CreatePedido(pedidoForCreationDto);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task CreatePedido_SaborNaoExisteNaFonteDados_RetornaBadRequestResponse()
        {
            var saborNome = "Teste";

            var nullSabor = new Sabor();
            nullSabor = null;

            _saborRepository.Setup(t => t.GetSabor(saborNome)).Returns(Task.FromResult(nullSabor));

            var pedidoForCreationDto = new PedidoForCreationDto { Tamanho = _tamanhoNome, Sabor = saborNome };

            var result = await _controller.CreatePedido(pedidoForCreationDto);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeletePedido_PedidoNaoEstaNaFonteDados_RetornaNotFoundResponse()
        {
            var newPedidoId = new Guid();

            var nullPedido = new Pedido();

            nullPedido = null;

            _pedidoRepository.Setup(s => s.GetPedido(newPedidoId)).Returns(Task.FromResult(nullPedido));

            var result = await _controller.DeletePedido(newPedidoId);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeletePedido_AoSerChamado_DeletaPedidoERetornaNoContentResponse()
        {
            var newPedidoId = new Guid();

            _pedidoRepository.Setup(s => s.GetPedido(newPedidoId)).Returns(Task.FromResult(new Pedido()));
            _pedidoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(true));

            var result = await _controller.DeletePedido(newPedidoId);

            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeletePedido_FalhaAoDeletarPedido_RetornaBadRequestResponse()
        {
            var newPedidoId = new Guid();

            _pedidoRepository.Setup(s => s.GetPedido(newPedidoId)).Returns(Task.FromResult(new Pedido()));
            _pedidoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(false));

            var result = await _controller.DeletePedido(newPedidoId);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

    }
}