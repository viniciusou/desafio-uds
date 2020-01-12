using System;
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
    public class TamanhosController : ControllerBase
    {
        private readonly ITamanhoRepository _repository;
        private readonly IMapper _mapper;

        public TamanhosController(ITamanhoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTamanhos()
        {
            var tamanhos = await _repository.GetTamanhos();

            return Ok(tamanhos);
        }

        [HttpGet("{id}", Name = "GetTamanho")]
        public async Task<IActionResult> GetTamanho(Guid id)
        {
            var tamanho = await _repository.GetTamanho(id);

            return Ok(tamanho);
        }

        [HttpPost]
        public async Task<IActionResult> AddTamanho(TamanhoForCreationDto tamanhoForCreationDto)
        {
            if (await _repository.TamanhoExists(tamanhoForCreationDto.Nome.ToLower()))
                return BadRequest("Tamanho ja existe");

            var tamanho = _mapper.Map<Tamanho>(tamanhoForCreationDto);

            _repository.Add(tamanho);

            if (await _repository.SaveAll())
                return CreatedAtRoute("GetTamanho", new { Id = tamanho.Id }, tamanho);

            return BadRequest("Falha ao tentar criar tamanho");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTamanho(Guid id)
        {
            var tamanho = await _repository.GetTamanho(id);

            if (tamanho == null)
                return NotFound();

            _repository.Delete(tamanho);

            if (await _repository.SaveAll())
                return NoContent();

            return BadRequest("Falha ao tentar remover tamanho");
        }

    }
}