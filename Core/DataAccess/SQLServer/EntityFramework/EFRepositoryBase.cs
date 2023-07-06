using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.SQLServer.EntityFramework
{
    public class EFRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            // AppDbContext context = new();
            // contex.Add(entity)
            // using directive
            using TContext context = new();
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using TContext context = new();
            var deleteEntity = context.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            using TContext context = new();
            return context.Set<TEntity>().FirstOrDefault(expression);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null)
        {
            using TContext context = new();
            return expression == null 
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(expression).ToList();
        }

        public void Update(TEntity entity)
        {
            using TContext context = new();
            var updateEntity = context.Entry(entity);
            updateEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
