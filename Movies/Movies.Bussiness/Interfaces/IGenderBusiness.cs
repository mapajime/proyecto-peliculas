using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface IGenderBusiness
    {
        Task CreateGenderAsync(Gender gender);

        Task UpdateGenderByIdAsync(Gender gender); 

         Task<IEnumerable<Gender>> GetGenderByNameAsync(string name);

        Task DeleteGenderAsync(Guid id);
    }
}