using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using reviewsapp.dto;

namespace reviewsapp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles ()
        {
            CreateMap<Model, Modeldto>();
        }
    }
}
