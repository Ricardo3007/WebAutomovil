using AutoMapper;
using WebAutomovil.Models;
using WebAutomovil.Models.DTO;

namespace WebAutomovil.AutoMapper
{
    public class CarroMapper:Profile
    {
        public CarroMapper()
        {
            CreateMap<CarroSetDTO, Carro>().ReverseMap();
            CreateMap<ClienteSetDTO, Cliente>().ReverseMap();
            CreateMap<ClienteCarroDTO, ClienteCarro>().ReverseMap();
        }
    }
}
