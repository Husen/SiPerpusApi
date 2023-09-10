using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using SiPerpusApi.Dto.ViewModel;
using SiPerpusApi.Exceptions;
using SiPerpusApi.Repositories;
using SiPerpusApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    // 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SI-PERPUS Private API", Version = "v1" });
    
    // xml summary
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

// register dbContext
builder.Services.AddDbContext<AppDbContext>();

// singleton, scoped, transient
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPersistence, DbPersistence>();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler(options =>
    {
        options.Run(async context =>
        {
            var exception = context.Features.Get<IExceptionHandlerPathFeature>();
            var errorRespnose = new ErrorResponse();
            context.Response.ContentType = "application/json";

            if (exception?.Error is NotFoundException)
            {
                errorRespnose.Code = "E-001";
                errorRespnose.Status = "Failed";
                errorRespnose.Message = exception?.Error?.Message;

                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(errorRespnose);
                return;
            }
            
            errorRespnose.Code = "E-005";
            errorRespnose.Status = "Error";
            errorRespnose.Message = exception?.Error.Message;
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(errorRespnose);
            
        });
    });

app.MapControllers();

app.Run();