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
    public class SaboresControllerTests
    {
        private Mock<ISaborRepository> _saborRepository;
        private Mock<IMapper> _mapper;
        private SaboresController _controller;
        private Guid _saborId;
        private Sabor _sabor;

        [SetUp]
        public void SetUp()
        {
            _saborRepository = new Mock<ISaborRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new SaboresController(_saborRepository.Object, _mapper.Object);

            _saborId = new Guid();
            _sabor = new Sabor { Id = _saborId, Nome = "Teste" };
            _saborRepository.Setup(s => s.GetSabor(_saborId)).Returns(Task.FromResult(_sabor));
        }

        [Test]
        public async Task GetSabores_AoSerChamado_ObtemSaboresDaFonteDados()
        {
            await _controller.GetSabores();

            _saborRepository.Verify(s => s.GetSabores());
        }

        [Test]
        public async Task GetSabores_AoSerChamado_RetornaOkResponse()
        {
            var result = await _controller.GetSabores();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetSabor_AoSerChamado_ObtemSaborDaFonteDados()
        {
            await _controller.GetSabor(_saborId);

            _saborRepository.Verify(s => s.GetSabor(_saborId));
        }

        [Test]
        public async Task GetSabor_AoSerChamado_RetornaOkResponse()
        {
            var result = await _controller.GetSabor(_saborId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task AddSabor_SaborJaExiste_RetornaBadRequestResponse()
        {
            _saborRepository.Setup(s => s.SaborExists(_sabor.Nome.ToLower())).Returns(Task.FromResult(true));
            var saborForCreationDto = new SaborForCreationDto { Nome = _sabor.Nome };

            var result = await _controller.AddSabor(saborForCreationDto);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task AddSabor_AoSerChamado_RetornaCreatedAtRouteResponse()
        {
            _saborRepository.Setup(s => s.SaborExists(_sabor.Nome.ToLower())).Returns(Task.FromResult(false));

            var saborForCreationDto = new SaborForCreationDto { Nome = "NovoTeste" };
            var sabor = new Sabor { Id = new Guid(), Nome = "NovoTeste" };

            _mapper.Setup(m => m.Map<Sabor>(saborForCreationDto)).Returns(sabor);

            _saborRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(true));

            var result = await _controller.AddSabor(saborForCreationDto);

            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
        }

        [Test]
        public async Task AddSabor_FalhaAoSalvarSabor_RetornaCreatedAtRouteResponse()
        {
            _saborRepository.Setup(s => s.SaborExists(_sabor.Nome.ToLower())).Returns(Task.FromResult(false));

            var saborForCreationDto = new SaborForCreationDto { Nome = "NovoTeste" };
            var sabor = new Sabor { Id = new Guid(), Nome = "NovoTeste" };

            _mapper.Setup(m => m.Map<Sabor>(saborForCreationDto)).Returns(sabor);

            _saborRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(false));

            var result = await _controller.AddSabor(saborForCreationDto);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeleteSabor_SaborNaoEstaNaFonteDados_RetornaNotFoundResponse()
        {
            var newSaborId = new Guid();

            var nullSabor = new Sabor();

            nullSabor = null;

            _saborRepository.Setup(s => s.GetSabor(newSaborId)).Returns(Task.FromResult(nullSabor));

            var result = await _controller.DeleteSabor(newSaborId);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteSabor_AoSerChamado_DeletaSaborERetornaNoContentResponse()
        {
            var newSaborId = new Guid();

            _saborRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(true));

            var result = await _controller.DeleteSabor(newSaborId);

            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteSabor_FalhaAoDeletarSabor_RetornaBadRequestResponse()
        {
            var newSaborId = new Guid();

            _saborRepository.Setup(s => s.SaveAll()).Returns(Task.FromResult(false));

            var result = await _controller.DeleteSabor(newSaborId);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

    }
}