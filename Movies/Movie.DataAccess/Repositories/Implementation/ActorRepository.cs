using Movies.DataAccess.Context;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;

namespace Movies.DataAccess.Repositories.Implementation
{
    public class ActorRepository : BaseRepository<Actor>, IActorRepository
    {
        public ActorRepository(MovieContext context) : base(context)
        {

        }
    }
}