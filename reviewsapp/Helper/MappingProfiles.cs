using AutoMapper;
using reviewsapp.dto;
using reviewsapp.models;

namespace reviewsapp.Helper
{
    public class MappingProfiles : Profile
    {// you can use AutoMapper to automatically map objects of one type to objects of another type.
        public MappingProfiles ()
        {
            CreateMap<Model, ModelDto>();//map properties from model to model class
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<OwnerName,OwnerNamesDto>();
            CreateMap<Review,ReviewsDto>();
            CreateMap<Reviewer,ReviewerDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CountryDto, Country>();
            CreateMap<OwnerNamesDto, OwnerName>();
            CreateMap<ModelDto, Model>();
            CreateMap<ReviewsDto, Review>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
