using System.Linq.Expressions;

namespace SiPerpusApi.Repositories;

public interface IRepository<TEntity>
{
    TEntity Save(TEntity entity);
    TEntity? FindById(int id);
    TEntity? FindBy(Expression<Func<TEntity, bool>> criteria);
    IQueryable<TEntity> FindAll();
    IEnumerable<TEntity> FindAll(Func<TEntity, bool> predicate);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}