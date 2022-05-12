using Microsoft.EntityFrameworkCore;
using SouthWestTradersAPI.Domain.IRepository;
using SouthWestTradersAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        protected readonly SouthWestTradersDBContext context;
        protected readonly DbSet<TEntity> dbset;

        public BaseRepository(SouthWestTradersDBContext context)
        {
            this.context = context;
            dbset = context.Set<TEntity>();
        }
        
        public async Task<TEntity> AddAsync(TEntity entity)
        {
           await dbset.AddAsync(entity);
           context.SaveChanges();
           return entity;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await dbset.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await dbset.Where(expression).ToListAsync();
        }

        public async Task RemoveAsync(Expression<Func<TEntity, bool>> expression)
        {
            var response = await dbset.Where(expression).FirstOrDefaultAsync();
            if (response == null)
                return;

            dbset.Remove(response);
            context.SaveChanges();
        }

        public async Task<TEntity?> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
        { 
            var response = await dbset.Where(expression).FirstOrDefaultAsync();
            if (response == null)
               return null;

            dbset.Update(entity);
            context.SaveChanges();
            return entity;

        }
    }
}
