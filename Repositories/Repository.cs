using System.Linq.Expressions;

namespace SiPerpusApi.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TEntity Save(TEntity entity)
    {
        var entry = _dbContext.Set<TEntity>().Add(entity);
        return entry.Entity;
    }

    public TEntity? FindById(int id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }

    public TEntity? FindBy(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
    }

    public IQueryable<TEntity> FindAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public IEnumerable<TEntity> FindAll(Func<TEntity, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }
}