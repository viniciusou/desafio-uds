using System;
using System.Linq;
using Acais.API.Dtos;
using Acais.API.Models;
using AutoMapper;

namespace Acais.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TamanhoForCreationDto, Tamanho>();

            CreateMap<SaborForCreationDto, Sabor>();

            CreateMap<PersonalizacaoForCreationDto, Personalizacao>();

            CreateMap<Personalizacao, PersonalizacaoToReturnDto>();

            CreateMap<PedidoPersonalizacaoForCreationDto, PedidoPersonalizacao>();

            CreateMap<PedidoPersonalizacao, PedidoPersonalizacaoToReturnDto>();

            CreateMap<Pedido, PedidoToReturnDto>()
                .ForMember(dest => dest.Tamanho, opt => opt.MapFrom(src => src.Tamanho.Nome))
                .ForMember(dest => dest.Sabor, opt => opt.MapFrom(src => src.Sabor.Nome))
                .ForMember(dest => dest.Personalizacao, opt => opt.MapFrom(src => src.PedidoPersonalizacoes.Select(p => $"{p.Personalizacao.Produto}: R${p.Personalizacao.Valor.ToString("0.00")}")));
        }
    }
}