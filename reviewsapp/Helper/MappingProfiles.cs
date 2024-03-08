using AutoMapper;
using reviewsapp.dto;
using reviewsapp.models;

namespace reviewsapp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles ()
        {
            CreateMap<Model, Modeldto>();
            CreateMap<Category, Categorydto>();
            
        }
    }
}
