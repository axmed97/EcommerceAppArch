using System.Linq.Expressions;

namespace Core.DataAccess
{
    // Generic Types
    public interface IRepositoryBase<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        // For using Lambda you must use Expression
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null);
    }
}
