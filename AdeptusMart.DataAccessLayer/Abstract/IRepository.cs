using System.Collections.Generic;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;

namespace AdeptusMart02.DataAccessLayer.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);        
    }
}
