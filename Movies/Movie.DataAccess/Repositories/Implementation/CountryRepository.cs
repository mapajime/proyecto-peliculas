using Movies.DataAccess.Context;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;

namespace Movies.DataAccess.Repositories.Implementation
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {

        public CountryRepository(MovieContext context) : base(context)
        {

        }
    }
}