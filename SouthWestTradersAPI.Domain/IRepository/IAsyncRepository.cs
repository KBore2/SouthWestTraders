using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Domain.IRepository
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> ListAsync(Expression<Func<TEntity,bool>> expression);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);

        Task RemoveAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity?> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity);


    }
}
