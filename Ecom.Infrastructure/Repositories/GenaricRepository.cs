using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Repositories
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity<int>
    {
        protected readonly AppDbContext _context;

        public GenaricRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            var count = await _context.Set<T>().CountAsync();
            return count;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
          var entities=  await _context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();

        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = _context.Set<T>().FindAsync(id);
            return await entity;
        }

        public Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
           var entity = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    entity = entity.Include(include);
                }
            }
            return entity.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
