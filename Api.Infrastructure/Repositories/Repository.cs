using Api.Domain.Repositories;
using Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
            => await _dbSet.AddRangeAsync(entity);

        public void Delete(T entity)
            => _dbSet.Remove(entity);

        public async Task<List<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public async Task Update(T entity)
            => await Task.Run(() => _dbSet.Update(entity));
    }
}
