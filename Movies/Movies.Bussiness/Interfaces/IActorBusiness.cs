using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface IActorBusiness
    {
        Task<Actor> CreateActorAsync(Actor actor);

        Task UpdateActorByIdAsync(Actor actor);

        Task<IEnumerable<Actor>> GetAllActorsAsync();

        Task<Actor> GetActorByIdAsync(Guid id);

        Task DeleteActorAsync(Guid id);

        Task<IEnumerable<Actor>> GetActorByLastNameAsync(string lastName);
        Task<int> GetActorCountAsync();
    }
}