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
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ITamanhoRepository _tamanhoRepository;
        private readonly ISaborRepository _saborRepository;
        private readonly IMapper _mapper;

        public PedidosController(IPedidoRepository pedidoRepository,
            ITamanhoRepository tamanhoRepository, ISaborRepository saborRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _tamanhoRepository = tamanhoRepository;
            _saborRepository = saborRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPedidos()
        {
            var pedidos = await _pedidoRepository.GetPedidos();

            var pedidosToReturn = _mapper.Map<List<PedidoToReturnDto>>(pedidos);

            return Ok(pedidosToReturn);
        }

        [HttpGet("{id}", Name = "GetPedido")]
        public async Task<IActionResult> GetPedido(Guid id)
        {
            var pedido = await _pedidoRepository.GetPedido(id);

            var pedidoToReturnDto = _mapper.Map<PedidoToReturnDto>(pedido);

            return Ok(pedidoToReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePedido(PedidoForCreationDto pedidoForCreationDto)
        {
            var tamanho = await _tamanhoRepository.GetTamanho(pedidoForCreationDto.Tamanho.ToLower());

            if (tamanho == null)
                return BadRequest("Tamanho nao encontrado.");

            var sabor = await _saborRepository.GetSabor(pedidoForCreationDto.Sabor.ToLower());

            if (sabor == null)
                return BadRequest("Sabor nao encontrado.");

            var pedidoToCreate = new Pedido
            {
                Tamanho = tamanho,
                Sabor = sabor,
                Tempo = tamanho.TempoPreparo + sabor.TempoPreparo,
                Valor = tamanho.Valor
            };

            var pedido = await _pedidoRepository.RegisterPedido(pedidoToCreate);

            var pedidoToReturnDto = _mapper.Map<PedidoToReturnDto>(pedido);

            return CreatedAtRoute("GetPedido", new { id = pedidoToReturnDto.Id }, pedidoToReturnDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(Guid id)
        {
            var pedido = await _pedidoRepository.GetPedido(id);

            if (pedido == null)
                return NotFound();

            _pedidoRepository.Delete(pedido);

            if (await _pedidoRepository.SaveAll())
                return NoContent();

            return BadRequest("Falha ao tentar remover pedido");
        }
    }
}