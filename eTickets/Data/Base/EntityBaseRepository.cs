using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Your implementation here
            return await Task.FromResult(Enumerable.Empty<T>());
        }

        public async Task<T> GetByIdAsync(int id)
        {
            // Your implementation here
            return await Task.FromResult<T>(null!);
        }

        public async Task AddAsync(T entity)
        {
            // Your implementation here
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(int id, T entity)
        {
            // Your implementation here
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            // Your implementation here
            await Task.CompletedTask;
        }
    }
}
