using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;

using AdeptusMart02.DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdeptusMart.DataAccess.Concrete
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly AdeptusMartDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(AdeptusMartDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            Console.WriteLine("AddAsync çalıştı");
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

       
    }
}
