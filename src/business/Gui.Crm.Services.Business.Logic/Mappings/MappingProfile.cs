using AutoMapper;
using Gui.Crm.Services.Data.Entities;
using Gui.Crm.Services.Shared.Dtos;

namespace Gui.Crm.Services.Business.Logic.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, DtoCategory>()
                .ForMember(dest => dest.Id, source => source.MapFrom(source => source.CategoryId)).ReverseMap();
        }
    }
}
