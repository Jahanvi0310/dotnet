using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T:class,IEntityBase,new()
    {
Task<IEnumerable<T>> GetAllAsync();
//to get the actor by id
Task<T> GetByIdAsync(int Id);
//to add the actor in database
Task AddAsync(T entity);
//to update the actor
Task UpdateAsync(int id,T entity);
//to delete the actor
Task DeleteAsync(int id);
    }
}