using System.Linq.Expressions;

namespace SiPerpusApi.Helpers;

public static class PaginationHelper
{
    public static IQueryable<TEntity> ApplySorting<TEntity>(IQueryable<TEntity> query, string sortBy, string sortDirection)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, sortBy);
        var lambda = Expression.Lambda(property, parameter);

        var methodName = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy";
        var genericMethod = typeof(Queryable).GetMethods()
            .Where(method => method.Name == methodName && method.IsGenericMethodDefinition)
            .Where(method => method.GetParameters().Length == 2)
            .Single()
            .MakeGenericMethod(typeof(TEntity), property.Type);

        return (IQueryable<TEntity>)genericMethod.Invoke(genericMethod, new object[] { query, lambda });
    }
}