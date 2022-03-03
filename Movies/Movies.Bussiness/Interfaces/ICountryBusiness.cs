using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface ICountryBusiness
    {
        Task CreateCountryAsync(Country country);

        Task UpdateCountryByIdAsync(Country country);

        Task DeleteCountryAsync(Guid id);

        Task<IEnumerable<Country>> GetCountryByName(string name);
    }
}