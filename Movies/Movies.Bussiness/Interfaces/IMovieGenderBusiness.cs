using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface IMovieGenderBusiness
    {
        Task CreateGenderMovieAsync(MovieGender movieGender);

        Task UpdateGenderMovieByIdAsync(MovieGender gender);

        Task DeleteGenderMovieAsync(Guid id);

        Task<IEnumerable<MovieGender>> GetGenderMovieByNameAsync(string name);
    }
}