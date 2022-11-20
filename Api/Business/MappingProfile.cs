using AutoMapper;
using Classes.DTO.Card;

namespace Api.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Card, CardItem>();
  
        }
    }
}