using Movies.DataAccess.Context;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;

namespace Movies.DataAccess.Repositories.Implementation
{
    public class GenderRepository : BaseRepository<Gender>, IGenderRepository
    {
        public GenderRepository(MovieContext context) : base(context)
        {

        }
    }
}