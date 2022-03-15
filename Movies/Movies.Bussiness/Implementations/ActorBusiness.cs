using Movies.Business.Interfaces;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Implementations
{
    public class ActorBusiness : IActorBusiness
    {
        private readonly IActorRepository _actorRepository;

        public ActorBusiness(IActorRepository repository)
        {
            _actorRepository = repository;
        }

        public async Task<Actor> CreateActorAsync(Actor actor)
        {
            ValidateActor(actor);
            await _actorRepository.AddAsync(actor);
            return actor;
        }

        public async Task DeleteActorAsync(Guid id) => await _actorRepository.DeleteAsync(id);

        public async Task<Actor> GetActorByIdAsync(Guid id) => await _actorRepository.GetByIdAsync(id);

        public async Task<int> GetActorCountAsync() => await _actorRepository.CountAsync();

        public async Task<IEnumerable<Actor>> GetActorByLastNameAsync(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return null;
            }
            return await _actorRepository.FilterAsync(l => l.LastName.Contains(lastName));
        }

        public async Task<IEnumerable<Actor>> GetAllActorsAsync() => await _actorRepository.GetAllAsync();

        public async Task UpdateActorByIdAsync(Actor actor)
        {
            ValidateActor(actor);
            await _actorRepository.UpdateAsync(actor);
        }

        private static void ValidateActor(Actor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(nameof(actor), "The actor is null");
            }
            if (string.IsNullOrEmpty(actor.FirstName) || string.IsNullOrEmpty(actor.LastName))
            {
                throw new NullReferenceException("The name or lastname of actor is empty");
            }
            if (!actor.DateOfBirth.HasValue)
            {
                throw new ArgumentException("The DateofBirth is null");
            }
        }
    }
}