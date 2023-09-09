﻿namespace SiPerpusApi.Services;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<ICategoryService, CategoryService>();
    }
}