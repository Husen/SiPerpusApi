using SiPerpusApi.Dto;
using SiPerpusApi.Exceptions;
using SiPerpusApi.Helpers;
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
            throw new Exception("Failed to create category");
        }
    }

    public CategoryResponse GetById(int id)
    {
        var category = _repository.FindById(id);
        if (category is null) throw new NotFoundException("id not found");
        var responseCategory = new CategoryResponse()
        {
            Id = category.Id,
            NameCategory = category.NameCategory,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
        return responseCategory;
    }

    public ApiPaginationResponse<List<CategoryResponse>> GetAll(RequestPagination requestPagination)
    {
        try
        {
            IQueryable<Category> query = _repository.FindAll();

            if (!string.IsNullOrWhiteSpace(requestPagination.SearchQuery))
            {
                var searchQueryLower = requestPagination.SearchQuery.ToLower();
                query = query.Where(c => c.NameCategory.ToLower().Contains(searchQueryLower));
            }

            if (!string.IsNullOrWhiteSpace(requestPagination.SortBy))
            {
                query = PaginationHelper.ApplySorting(query, requestPagination.SortBy, requestPagination.SortDirection);
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Failed to get all categories");
        }
    }
    
    public CategoryResponse UpdateCategory(int id, CategoryRequest categoryRequest)
    {
        var category = _repository.FindById(id);
        if (category is null) throw new NotFoundException("id not found");
        category.NameCategory = categoryRequest.NameCategory;
        category.UpdatedAt = DateTime.UtcNow;
            
        _repository.Update(category);
        _persistence.SaveChanges();
        var newCategory = _repository.FindById(id);

        var responseCategory = new CategoryResponse()
        {
            Id = newCategory.Id,
            NameCategory = newCategory.NameCategory,
            CreatedAt = newCategory.CreatedAt,
            UpdatedAt = newCategory.UpdatedAt
        };
        return responseCategory;
    }

    public void DeleteCategory(int id)
    {
        var category = _repository.FindById(id);
        if (category is null) throw new NotFoundException("id not found");
        _repository.Delete(category);
        _persistence.SaveChanges();
    }

}