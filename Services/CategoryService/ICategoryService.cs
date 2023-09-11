using SiPerpusApi.Dto;
using SiPerpusApi.Models;

namespace SiPerpusApi.Services;

public interface ICategoryService
{
    CategoryResponse CreateCategory(CategoryRequest categoryRequest);
    CategoryResponse GetById(int id);
    ApiPaginationResponse<List<CategoryResponse>> GetAll(RequestPagination requestPagination);
    CategoryResponse UpdateCategory(int id, CategoryRequest categoryRequest);
    void DeleteCategory(int id);
}