﻿using Movies.Business.Interfaces;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Business.Implementations
{
    public class MovieBusiness : IMovieBusiness
    {
        private readonly IMovieRepository _repository;

        public MovieBusiness(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateMovieAsync(Movie movie)
        {
            ValidateMovie(movie);
            await _repository.AddAsync(movie);
        }

        public async Task DeleteMovieAsync(Guid id) => await _repository.DeleteAsync(id);

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync() => await _repository.GetAllAsync();

        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return await _repository.FilterAsync(e => e.Name.Contains(name));
        }

        public async Task<int> GetNumberOfMoviesAsync() => await _repository.CountAsync();

        public async Task UpdateMovieAsync(Movie movie)
        {
            ValidateMovie(movie);
            await _repository.UpdateAsync(movie);
        }

        private static void ValidateMovie(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("The movie is null");
            }
            if (string.IsNullOrEmpty(movie.Name))
            {
                throw new NullReferenceException("The name of movie is empty");
            }
            if (movie.Cast == null || !movie.Cast.Any())
            {
                throw new ArgumentException("The movie has no actors");
            }
            if (string.IsNullOrEmpty(movie.Director))
            {
                throw new ArgumentException("The movie has no Director");
            }
        }
    }
}