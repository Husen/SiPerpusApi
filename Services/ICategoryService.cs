﻿using SiPerpusApi.Dto.CategoryDto;
using SiPerpusApi.Dto.ViewModel;
using SiPerpusApi.Models;

namespace SiPerpusApi.Services;

public interface ICategoryService
{
    CategoryResponse CreateCategory(CategoryRequest categoryRequest);
    CategoryResponse GetById(int id);
    ApiPaginationResponse<List<CategoryResponse>> GetAll(RequestPagination requestPagination);
}