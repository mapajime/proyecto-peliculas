using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface IMovieGenderBusiness
    {
        Task<MovieGender> CreateGenderMovieAsync(MovieGender movieGender);

        Task UpdateGenderMovieByIdAsync(MovieGender gender);

        Task DeleteGenderMovieAsync(Guid id);

        Task<IEnumerable<MovieGender>> GetGenderMovieByNameAsync(string name);
        Task<MovieGender> GetGenderMovieByIdAsync(Guid id);
    }
}