using AutoMapper;
using Movies.Api.Models;
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
            CreateMap<GenderModel, Gender>();
            CreateMap<LanguageModel, Language>();
            CreateMap<Language, LanguageModel>();
            CreateMap<MovieModel, Movie>();
            CreateMap<Movie, MovieModel>();
            CreateMap<MovieGenderModel, MovieGender>();
            CreateMap<MovieGender, MovieGenderModel>();
        }
    }
}