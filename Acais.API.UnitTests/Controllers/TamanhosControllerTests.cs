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
    public class TamanhosControllerTests
    {
        private Mock<ITamanhoRepository> _tamanhoRepository;
        private Mock<IMapper> _mapper;
        private TamanhosController _controller;
        private Guid _tamanhoId;
        private Tamanho _tamanho;

        [SetUp]
        public void SetUp()
        {
            _tamanhoRepository = new Mock<ITamanhoRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new TamanhosController(_tamanhoRepository.Object, _mapper.Object);

            _tamanhoId = new Guid();
            _tamanho = new Tamanho { Id = _tamanhoId, Nome = "Teste" };
            _tamanhoRepository.Setup(s => s.GetTamanho(_tamanhoId)).Returns(Task.FromResult(_tamanho));
        }

        [Test]
        public async Task GetTamanhos_AoSerChamado_ObtemTamanhosDaFonteDados()
        {
            await _controller.GetTamanhos();

            _tamanhoRepository.Verify(s => s.GetTamanhos());
        }

        [Test]
        public async Task GetTamanhos_AoSerChamado_RetornaOkResponse()
        {
            var result = await _controller.GetTamanhos();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetTamanho_AoSerChamado_ObtemTamanhoDaFonteDados()
        {
            await _controller.GetTamanho(_tamanhoId);

            _tamanhoRepository.Verify(s => s.GetTamanho(_tamanhoId));
        }

        [Test]
        public async Task GetTamanho_AoSerChamado_RetornaOkResponse()
        {
            var result = await _controller.GetTamanho(_tamanhoId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task AddTamanho_TamanhoJaExiste_RetornaBadRequestResponse()
        {
            _tamanhoRepository.Setup(s => s.TamanhoExists(_tamanho.Nome.ToLower())).Returns(Task.FromResult(true));
            var tamanhoForCreationDto = new TamanhoForCreationDto { Nome = _tamanho.Nome };

            var result = await _controller.AddTamanho(tamanhoForCreationDto);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task AddTamanho_AoSerChamado_RetornaCreatedAtRouteResponse()
        {
            _tamanhoRepository.Setup(s => s.TamanhoExists(_tamanho.Nome.ToLower())).Returns(Task.FromResult(false));

            var tamanhoForCreationDto = new TamanhoForCreationDto { Nome = "NovoTeste" };
            var tamanho = new Tamanho { Id = new Guid(), Nome = "NovoTeste" };

            _mapper.Setup(m => m.Map<Tamanho>(tamanhoForCreationDto)).Returns(tamanho);

            _tamanhoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(true));

            var result = await _controller.AddTamanho(tamanhoForCreationDto);

            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
        }

        [Test]
        public async Task AddTamanho_FalhaAoSalvarTamanho_RetornaCreatedAtRouteResponse()
        {
            _tamanhoRepository.Setup(s => s.TamanhoExists(_tamanho.Nome.ToLower())).Returns(Task.FromResult(false));

            var tamanhoForCreationDto = new TamanhoForCreationDto { Nome = "NovoTeste" };
            var tamanho = new Tamanho { Id = new Guid(), Nome = "NovoTeste" };

            _mapper.Setup(m => m.Map<Tamanho>(tamanhoForCreationDto)).Returns(tamanho);

            _tamanhoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(false));

            var result = await _controller.AddTamanho(tamanhoForCreationDto);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeleteTamanho_TamanhoNaoEstaNaFonteDados_RetornaNotFoundResponse()
        {
            var newTamanhoId = new Guid();

            var nulltamanho = new Tamanho();

            nulltamanho = null;

            _tamanhoRepository.Setup(s => s.GetTamanho(newTamanhoId)).Returns(Task.FromResult(nulltamanho));

            var result = await _controller.DeleteTamanho(newTamanhoId);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteTamanho_AoSerChamado_DeletaTamanhoERetornaNoContentResponse()
        {
            var newTamanhoId = new Guid();

            _tamanhoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(true));

            var result = await _controller.DeleteTamanho(newTamanhoId);

            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteTamanho_FalhaAoDeletarTamanho_RetornaBadRequestResponse()
        {
            var newTamanhoId = new Guid();

            _tamanhoRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(false));

            var result = await _controller.DeleteTamanho(newTamanhoId);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}