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
    public class SaboresController : ControllerBase
    {
        private readonly ISaborRepository _repository;
        private readonly IMapper _mapper;
        public SaboresController(ISaborRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSabores()
        {
            var sabores = await _repository.GetSabores();

            return Ok(sabores);
        }

        [HttpGet("{id}", Name = "GetSabor")]
        public async Task<IActionResult> GetSabor(Guid id)
        {
            var sabor = await _repository.GetSabor(id);

            return Ok(sabor);
        }

        [HttpPost]
        public async Task<IActionResult> AddSabor(SaborForCreationDto saborForCreationDto)
        {
            if (await _repository.SaborExists(saborForCreationDto.Nome.ToLower()))
                return BadRequest("Sabor ja existente");

            var sabor = _mapper.Map<Sabor>(saborForCreationDto);
            _repository.Add(sabor);

            if (await _repository.SaveAll())
                return CreatedAtRoute("GetSabor", new { id = sabor.Id }, sabor);

            return BadRequest("Falha ao tentar criar sabor");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSabor(Guid id)
        {
            var sabor = await _repository.GetSabor(id);

            if (sabor == null)
                return NotFound();

            _repository.Delete(sabor);

            if (await _repository.SaveAll())
                return NoContent();

            return BadRequest("Falha ao tentar remover sabor");
        }
    }
}