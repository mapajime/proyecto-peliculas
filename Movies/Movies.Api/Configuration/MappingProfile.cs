using AutoMapper;
using Movies.Models;
using Movies.Entities;

namespace Movies.Api.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActorModel, Actor>();
            CreateMap<Actor, ActorModel>();
            CreateMap<CountryModel, Country>();
            CreateMap<Country, CountryModel>();
            CreateMap<GenderModel, Gender>();
            CreateMap<Gender, GenderModel>();
            CreateMap<LanguageModel, Language>();
            CreateMap<Language, LanguageModel>();
            CreateMap<MovieModel, Movie>()
                .ForMember(dest => dest.MovieGenderId, o => o.MapFrom(s => s.GenderId))
                .ForMember(dest => dest.CountryId, o => o.MapFrom(s => s.CountryOfOriginId));
            CreateMap<Movie, MovieModel>()
                .ForMember(dest => dest.GenderId, o => o.MapFrom(s => s.MovieGenderId))
                .ForMember(dest => dest.CountryOfOriginId, o => o.MapFrom(s => s.CountryId));
            CreateMap<MovieGenderModel, MovieGender>();
            CreateMap<MovieGender, MovieGenderModel>();
        }
    }
}