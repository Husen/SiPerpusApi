using Microsoft.EntityFrameworkCore;

namespace SiPerpusApi.Repositories;

public class DbPersistence : IPersistence
{
    private readonly AppDbContext _dbContext;

    public DbPersistence(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public void BeginTransaction()
    {
        _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        _dbContext.Database.CommitTransaction();
    }

    public void Rollback()
    {
        _dbContext.Database.RollbackTransaction();
    }
}