using Movies.Business.Interfaces;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Implementations
{
    public class MovieGenderBusiness : IMovieGenderBusiness
    {
        private readonly IMovieGenderRepository _movieGenderRepository;

        public MovieGenderBusiness(IMovieGenderRepository movieGenderRepository)
        {
            _movieGenderRepository = movieGenderRepository;
        }

        public async Task CreateGenderMovieAsync(MovieGender movieGender)
        {
           ValidateMovieGender(movieGender);
            await _movieGenderRepository.AddAsync(movieGender);
        }

        public async Task DeleteGenderMovieAsync(Guid id) => await _movieGenderRepository.DeleteAsync(id);

        public async Task<IEnumerable<MovieGender>> GetGenderMovieByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return await _movieGenderRepository.FilterAsync(n => n.Name.Contains(name));
        }

        public async Task UpdateGenderMovieAsync(MovieGender gender)
        {
            ValidateMovieGender(gender);
            await _movieGenderRepository.UpdateAsync(gender);
        }

        private static void ValidateMovieGender(MovieGender gender)
        {
            if (string.IsNullOrEmpty(gender?.Name))
            {
                throw new ArgumentNullException("The movie gender cannont be null o empty");
            }
        }
    }
}