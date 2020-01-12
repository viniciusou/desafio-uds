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

            CreateMap<Pedido, PedidoToReturnDto>()
                .ForMember(dest => dest.Tamanho, opt => opt.MapFrom(src => src.Tamanho.Nome))
                .ForMember(dest => dest.Sabor, opt => opt.MapFrom(src => src.Sabor.Nome));
        }
    }
}