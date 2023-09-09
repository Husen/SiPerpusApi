using System.Linq.Expressions;
using SiPerpusApi.Dto.CategoryDto;
using SiPerpusApi.Dto.ViewModel;
using SiPerpusApi.Models;
using SiPerpusApi.Repositories;

namespace SiPerpusApi.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repository;
    private readonly IPersistence _persistence;

    public CategoryService(IRepository<Category> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public CategoryResponse CreateCategory(CategoryRequest categoryRequest)
    {
        try
        {
            var category = new Category()
            {
                NameCategory = categoryRequest.NameCategory
            };
            var newCategory = _repository.Save(category);
            _persistence.SaveChanges();

            var responseCategory = new CategoryResponse()
            {
                Id = newCategory.Id,
                NameCategory = newCategory.NameCategory,
                CreatedAt = newCategory.CreatedAt,
                UpdatedAt = newCategory.UpdatedAt
            };
            return responseCategory;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public CategoryResponse GetById(int id)
    {
        try
        {
            var category = _repository.FindById(id);
            if (category is null) return null;
            var responseCategory = new CategoryResponse()
            {
                Id = category.Id,
                NameCategory = category.NameCategory,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
            return responseCategory;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public ApiPaginationResponse<List<CategoryResponse>> GetAll(RequestPagination requestPagination)
    {
        IQueryable<Category> query = _repository.FindAll();

        if (!string.IsNullOrWhiteSpace(requestPagination.SearchQuery))
        {
            var searchQueryLower = requestPagination.SearchQuery.ToLower();
            query = query.Where(c => c.NameCategory.ToLower().Contains(searchQueryLower));
        }
        
        if (!string.IsNullOrWhiteSpace(requestPagination.SortBy))
        {
            query = ApplySorting(query, requestPagination.SortBy, requestPagination.SortDirection);
        }

        var totalRecords = query.Count();

        var skip = (requestPagination.Page - 1) * requestPagination.Limit;
        query = query.Skip((int)skip).Take(requestPagination.Limit.Value);

        var result = query.Select(c => new CategoryResponse
        {
            Id = c.Id,
            NameCategory = c.NameCategory,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        }).ToList();

        var totalPages = (int)Math.Ceiling((double)totalRecords / requestPagination.Limit.Value);

        var apiPaginationResponse = new ApiPaginationResponse<List<CategoryResponse>>
        {
            Page = requestPagination.Page.Value,
            Limit = requestPagination.Limit.Value,
            TotalRows = totalRecords,
            TotalPage = totalPages,
            Data = result
        };

        return apiPaginationResponse;
    }

    private IQueryable<TEntity> ApplySorting<TEntity>(IQueryable<TEntity> query, string sortBy, string sortDirection)
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