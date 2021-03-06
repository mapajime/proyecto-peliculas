using Movies.Business.Interfaces;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Implementations
{
    public class CountryBusiness : ICountryBusiness
    {
        private readonly ICountryRepository _countryRepository;

        public CountryBusiness(ICountryRepository repository)
        {
            _countryRepository = repository;
        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
            ValidateCountry(country);
            await _countryRepository.AddAsync(country);
            return country;
        }

        public async Task DeleteCountryAsync(Guid id) => await _countryRepository.DeleteAsync(id);

        public async Task<IEnumerable<Country>> GetCountriesByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return await _countryRepository.FilterAsync(n => n.Name == name);
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync() => await _countryRepository.GetAllAsync();

        public async Task UpdateCountryByIdAsync(Country country)
        {
            ValidateCountry(country);
            await _countryRepository.UpdateAsync(country);
        }

        public async Task<Country> GetCountryByIdAsync(Guid id) => await _countryRepository.GetByIdAsync(id);

        private static void ValidateCountry(Country country)
        {
            if (string.IsNullOrEmpty(country?.Name))
            {
                throw new ArgumentNullException(nameof(country), "The country cannont be null or empty");
            }
        }
    }
}