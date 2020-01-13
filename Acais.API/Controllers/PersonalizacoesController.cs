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
    public class PersonalizacoesController : ControllerBase
    {
        private readonly IPersonalizacaoRepository _repository;
        private readonly IMapper _mapper;
        public PersonalizacoesController(IPersonalizacaoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonalizacoes()
        {
            var personalizacoes = await _repository.GetPersonalizacoes();

            var personalizacoesToReturn = _mapper.Map<List<PersonalizacaoToReturnDto>>(personalizacoes);

            return Ok(personalizacoesToReturn);
        }

        [HttpGet("{id}", Name = "GetPersonalizacao")]
        public async Task<IActionResult> GetPersonalizacao(Guid id)
        {
            var personalizacao = await _repository.GetPersonalizacao(id);

            var personalizacaoToReturn = _mapper.Map<PersonalizacaoToReturnDto>(personalizacao); 

            return Ok(personalizacaoToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonalizacao(PersonalizacaoForCreationDto personalizacaoForCreationDto)
        {
            if (await _repository.PersonalizacaoExists(personalizacaoForCreationDto.Produto.ToLower()))
                return BadRequest("Personalizacao ja existe");

            var personalizacao = _mapper.Map<Personalizacao>(personalizacaoForCreationDto);

            _repository.Add(personalizacao);

            if (await _repository.SaveAll())
                return CreatedAtRoute("GetPersonalizacao", new { Id = personalizacao.Id }, personalizacao);

            return BadRequest("Falha ao tentar criar personalizacao");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonalizacao(Guid id)
        {
            var personalizacao = await _repository.GetPersonalizacao(id);

            if (personalizacao == null)
                return NotFound();

            _repository.Delete(personalizacao);

            if (await _repository.SaveAll())
                return NoContent();

            return BadRequest("Falha ao tentar remover personalizacao");
        }
    }
}