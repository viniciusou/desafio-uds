using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acais.API.Data.Interfaces;
using Acais.API.Dtos;
using Acais.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Acais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoPersonalizacoesController : ControllerBase
    {
        private readonly IPedidoPersonalizacaoRepository _repository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPersonalizacaoRepository _personalizacaoRepository;
        private readonly IMapper _mapper;

        public PedidoPersonalizacoesController(IPedidoPersonalizacaoRepository repository,
        IPedidoRepository pedidoRepository, IPersonalizacaoRepository personalizacaoRepository, IMapper mapper)
        {
            _repository = repository;
            _pedidoRepository = pedidoRepository;
            _personalizacaoRepository = personalizacaoRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePedidoPersonalizacao(PedidoPersonalizacaoForCreationDto pedidoPersonalizacaoForCreationDto)
        {
            var pedido = await _pedidoRepository.GetPedido(pedidoPersonalizacaoForCreationDto.PedidoId);

            if (pedido == null)
                return BadRequest("Pedido nao encontrado");

            var personalizacao = await _personalizacaoRepository.GetPersonalizacao(pedidoPersonalizacaoForCreationDto.PersonalizacaoId);

            if (personalizacao == null)
                return BadRequest("Personalizacao nao encontrada");


            var pedidoPersonalizacao = _mapper.Map<PedidoPersonalizacao>(pedidoPersonalizacaoForCreationDto);

            var pedidoPersonalizado = await _repository.RegisterPedidoPersonalizacao(pedidoPersonalizacao);


            var pedidoAtualizado = await _pedidoRepository.UpdatePedido(
                pedidoPersonalizacaoForCreationDto.PedidoId, personalizacao.TempoPreparo, personalizacao.Valor);

            var pedidoToReturn = _mapper.Map<PedidoToReturnDto>(pedidoAtualizado);

            return CreatedAtRoute("GetPedido", new { controller = "Pedidos", id = pedidoToReturn.Id }, pedidoToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetPedidoPersonalizacoes()
        {
            var pedidoPersonalizacoes = await _repository.GetPedidoPersonalizacoes();

            var pedidoPersonalizacoesToReturn = _mapper.Map<List<PedidoPersonalizacaoToReturnDto>>(pedidoPersonalizacoes);

            return Ok(pedidoPersonalizacoesToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoPersonalizacao(Guid id)
        {
            var pedidoPersonalizacao = await _repository.GetPedidoPersonalizacao(id);

            var pedidoPersonalizacaoToReturn = _mapper.Map<PedidoPersonalizacaoToReturnDto>(pedidoPersonalizacao);

            return Ok(pedidoPersonalizacaoToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoPersonalizacao(Guid id)
        {
            var pedidoPersonalizacao = await _repository.GetPedidoPersonalizacao(id);

            if (pedidoPersonalizacao == null)
                return NotFound();

            _repository.Delete(pedidoPersonalizacao);

            if (await _pedidoRepository.SaveAll())
                return NoContent();

            return BadRequest("Falha ao tentar remover pedido");
        }
    }
}