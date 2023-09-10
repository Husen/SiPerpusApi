﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SiPerpusApi.Dto.CategoryDto;
using SiPerpusApi.Dto.ViewModel;
using SiPerpusApi.Models;
using SiPerpusApi.Services;

namespace SiPerpusApi.Controllers;

[Route("api/category")]
[ApiExplorerSettings(GroupName = "v1")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([Required, FromBody] CategoryRequest categoryRequest)
    {
        CategoryResponse category = _categoryService.CreateCategory(categoryRequest);
        var response = new ApiResponse<CategoryResponse>
        {
            Code = 201,
            Message = "Category created successfully",
            Status = "S",
            Data = category
        };
        return Created("api/category", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        CategoryResponse category = _categoryService.GetById(id);

        var response = new ApiResponse<CategoryResponse>()
        {
            Code = 200,
            Message = "OK",
            Status = "OK",
            Data = category
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories([FromQuery] RequestPagination requestPagination)
    {
        var responseCategories = _categoryService.GetAll(requestPagination);
        return Ok(responseCategories);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryById(int id, [Required, FromBody] CategoryRequest categoryRequest)
    {
        CategoryResponse category = _categoryService.UpdateCategory(id, categoryRequest);
        
        var response = new ApiResponse<CategoryResponse>
        {
            Code = 200,
            Message = "Category update successfully",
            Status = "S",
            Data = category
        };
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryById(int id)
    {
        _categoryService.DeleteCategory(id);
        var response = new ApiResponse<object>
        {
            Code = 200,
            Message = "Category delete successfully",
            Status = "S",
            Data = new object[0]
        };
        return Ok(response);
    }
    
}