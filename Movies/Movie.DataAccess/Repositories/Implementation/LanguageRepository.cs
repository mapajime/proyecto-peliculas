using Movies.DataAccess.Context;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;

namespace Movies.DataAccess.Repositories.Implementation
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(MovieContext context) : base(context)
        {

        }
    }
}