using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;
namespace eTickets.Data.Services
{
    public interface IActorsService
    {
Task<IEnumerable<Actor>> GetAllAsync();
//to get the actor by id
Task<Actor?> GetByIdAsync(int Id);
//to add the actor in database
Task AddAsync(Actor actor);
//to update the actor
Actor Update(int id,Actor newActor);
//to delete the actor
void Delete(int id);

    }
}