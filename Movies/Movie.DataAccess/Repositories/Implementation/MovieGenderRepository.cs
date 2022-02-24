using Movies.DataAccess.Context;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;

namespace Movies.DataAccess.Repositories.Implementation
{
    public class MovieGenderRepository : BaseRepository<MovieGender>, IMovieGenderRepository
    {
        public MovieGenderRepository(MovieContext context) : base(context)
        {

        }
    }
}