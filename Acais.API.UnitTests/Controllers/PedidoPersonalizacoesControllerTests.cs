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
    public class PedidoPersonalizacoesControllerTests
    {
        private Mock<IPedidoPersonalizacaoRepository> _repository;
        private Mock<IPedidoRepository> _pedidoRepository;
        private Mock<IPersonalizacaoRepository> _personalizacaoRepository;
        private Mock<IMapper> _mapper;
        private PedidoPersonalizacoesController _controller;
        private Guid _pedidoId;
        private Pedido _pedido;
        private Guid _personalizacaoId;
        private Personalizacao _personalizacao;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IPedidoPersonalizacaoRepository>();
            _pedidoRepository = new Mock<IPedidoRepository>();
            _personalizacaoRepository = new Mock<IPersonalizacaoRepository>();
            _mapper = new Mock<IMapper>();

            _controller = new PedidoPersonalizacoesController(_repository.Object,
                _pedidoRepository.Object, _personalizacaoRepository.Object, _mapper.Object);

            _pedidoId = new Guid();
            _pedido = new Pedido { Id = _pedidoId };
            var pedidos = new List<Pedido> { _pedido };

            _pedidoRepository.Setup(t => t.GetPedido(_pedidoId)).Returns(Task.FromResult(_pedido));
            _pedidoRepository.Setup(t => t.GetPedidos()).Returns(Task.FromResult(pedidos.AsEnumerable()));

            _personalizacaoId = new Guid();
            _personalizacao = new Personalizacao { Id = _personalizacaoId, Produto = "Teste" };
            var personalizacoes = new List<Personalizacao> { _personalizacao };

            _personalizacaoRepository.Setup(s => s.GetPersonalizacao(_personalizacaoId)).Returns(Task.FromResult(_personalizacao));
            _personalizacaoRepository.Setup(s => s.GetPersonalizacoes()).Returns(Task.FromResult(personalizacoes.AsEnumerable()));
        }

        [Test]
        public async Task DeletePedidoPersonalizacao_PedidoPersonalizacaoNaoEstaNaFonteDados_RetornaNotFoundResponse()
        {
            var newPedidoPersonalizacaoId = new Guid();

            var nullPedidoPersonalizacao = new PedidoPersonalizacao();

            nullPedidoPersonalizacao = null;

            _repository.Setup(s => s.GetPedidoPersonalizacao(newPedidoPersonalizacaoId)).Returns(Task.FromResult(nullPedidoPersonalizacao));

            var result = await _controller.DeletePedidoPersonalizacao(newPedidoPersonalizacaoId);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeletePedidoPersonalizacao_FalhaAoDeletarPedidoPersonalizacao_RetornaBadRequestResponse()
        {
            var newPedidoPersonalizacaoId = new Guid();

            _repository.Setup(s => s.GetPedidoPersonalizacao(newPedidoPersonalizacaoId)).Returns(Task.FromResult(new PedidoPersonalizacao()));
            _repository.Setup(s => s.SaveAll()).Returns(Task.FromResult(false));

            var result = await _controller.DeletePedidoPersonalizacao(newPedidoPersonalizacaoId);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}