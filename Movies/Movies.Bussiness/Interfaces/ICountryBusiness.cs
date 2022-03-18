using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface ICountryBusiness
    {
        Task<Country> CreateCountryAsync(Country country);

        Task UpdateCountryByIdAsync(Country country);

        Task DeleteCountryAsync(Guid id);

        Task<IEnumerable<Country>> GetCountriesByNameAsync(string name);

        Task<IEnumerable<Country>> GetAllCountriesAsync();

        Task<Country> GetCountryByIdAsync(Guid id);
    }
}