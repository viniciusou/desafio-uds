using System;
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
    public class PersonalizacoesControllerTests
    {
        private Mock<IPersonalizacaoRepository> _personalizacaoRepository;
        private Mock<IMapper> _mapper;
        private PersonalizacoesController _controller;
        private Guid _personalizacaoId;
        private Personalizacao _personalizacao;

        [SetUp]
        public void SetUp()
        {
            _personalizacaoRepository = new Mock<IPersonalizacaoRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new PersonalizacoesController(_personalizacaoRepository.Object, _mapper.Object);

            _personalizacaoId = new Guid();
            _personalizacao = new Personalizacao { Id = _personalizacaoId, Produto = "Teste" };
            _personalizacaoRepository.Setup(s => s.GetPersonalizacao(_personalizacaoId)).Returns(Task.FromResult(_personalizacao));
        }

        [Test]
        public async Task GetPersonalizacaoes_AoSerChamado_ObtemPersonalizacaoesDaFonteDados()
        {
            await _controller.GetPersonalizacoes();

            _personalizacaoRepository.Verify(s => s.GetPersonalizacoes());
        }

        [Test]
        public async Task GetPersonalizacoes_AoSerChamado_RetornaOkResponse()
        {
            var result = await _controller.GetPersonalizacoes();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetPersonalizacao_AoSerChamado_ObtemPersonalizacaoDaFonteDados()
        {
            await _controller.GetPersonalizacao(_personalizacaoId);

            _personalizacaoRepository.Verify(s => s.GetPersonalizacao(_personalizacaoId));
        }

        [Test]
        public async Task GetPersonalizacao_AoSerChamado_RetornaOkResponse()
        {
            var result = await _controller.GetPersonalizacao(_personalizacaoId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task AddPersonalizacao_PersonalizacaoJaExiste_RetornaBadRequestResponse()
        {
            _personalizacaoRepository.Setup(s => s.PersonalizacaoExists(_personalizacao.Produto.ToLower())).Returns(Task.FromResult(true));
            var personalizacaoForCreationDto = new PersonalizacaoForCreationDto { Produto = _personalizacao.Produto };

            var result = await _controller.AddPersonalizacao(personalizacaoForCreationDto);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task AddPersonalizacao_AoSerChamado_RetornaCreatedAtRouteResponse()
        {
            _personalizacaoRepository.Setup(s => s.PersonalizacaoExists(_personalizacao.Produto.ToLower())).Returns(Task.FromResult(false));

            var personalizacaoForCreationDto = new PersonalizacaoForCreationDto { Produto = "NovoTeste" };
            var personalizacao = new Personalizacao { Id = new Guid(), Produto = "NovoTeste" };

            _mapper.Setup(m => m.Map<Personalizacao>(personalizacaoForCreationDto)).Returns(personalizacao);

            _personalizacaoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(true));

            var result = await _controller.AddPersonalizacao(personalizacaoForCreationDto);

            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
        }

        [Test]
        public async Task AddPersonalizacao_FalhaAoSalvarPersonalizacao_RetornaCreatedAtRouteResponse()
        {
            _personalizacaoRepository.Setup(s => s.PersonalizacaoExists(_personalizacao.Produto.ToLower())).Returns(Task.FromResult(false));

            var personalizacaoForCreationDto = new PersonalizacaoForCreationDto { Produto = "NovoTeste" };
            var personalizacao = new Personalizacao { Id = new Guid(), Produto = "NovoTeste" };

            _mapper.Setup(m => m.Map<Personalizacao>(personalizacaoForCreationDto)).Returns(personalizacao);

            _personalizacaoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(false));

            var result = await _controller.AddPersonalizacao(personalizacaoForCreationDto);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeletePersonalizacao_PersonalizacaoNaoEstaNaFonteDados_RetornaNotFoundResponse()
        {
            var newPersonalizacaoId = new Guid();

            var nullPersonalizacao = new Personalizacao();

            nullPersonalizacao = null;

            _personalizacaoRepository.Setup(s => s.GetPersonalizacao(newPersonalizacaoId)).Returns(Task.FromResult(nullPersonalizacao));

            var result = await _controller.DeletePersonalizacao(newPersonalizacaoId);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeletePersonalizacao_AoSerChamado_DeletaPersonalizacaoERetornaNoContentResponse()
        {
            var newPersonalizacaoId = new Guid();

            _personalizacaoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(true));

            var result = await _controller.DeletePersonalizacao(newPersonalizacaoId);

            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeletePersonalizacao_FalhaAoDeletarPersonalizacao_RetornaBadRequestResponse()
        {
            var newPersonalizacaoId = new Guid();

            _personalizacaoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(false));

            var result = await _controller.DeletePersonalizacao(newPersonalizacaoId);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}