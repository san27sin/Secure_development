using AutoMapper;
using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Models.Requests;

namespace CardStorageService.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<CreateCardRequest, Card>().ReverseMap();
            CreateMap<CardDto, Card>().ReverseMap();

            CreateMap<CreateClientRequest, Client>().ReverseMap();
            CreateMap<ClientDto, Client>().ReverseMap();
        }
    }
}
