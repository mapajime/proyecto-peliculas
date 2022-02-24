using Movies.DataAccess.Context;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;

namespace Movies.DataAccess.Repositories.Implementation
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieContext context) : base(context)
        {

        }
    }
}