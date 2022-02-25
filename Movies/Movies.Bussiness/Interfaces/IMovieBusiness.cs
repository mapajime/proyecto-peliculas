using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface IMovieBusiness
    {
        Task CreateMovieAsync(Movie movie);

        Task UpdateMovieAsync(Movie movie);

        Task<Movie> GetMovieByIdAsync(Guid id);

        Task DeleteMovieAsync(Guid id);

        Task<IEnumerable<Movie>> GetAllMoviesAsync();

        Task<int> GetNumberOfMoviesAsync();

        Task<IEnumerable<Movie>> GetMoviesByNameAsync(string name);
    }
}