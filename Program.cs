using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SiPerpusApi.Dto;
using SiPerpusApi.Exceptions;
using SiPerpusApi.Repositories;
using SiPerpusApi.Security;
using SiPerpusApi.Services;
using SiPerpusApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SI-PERPUS Public API", Version = "v1" });
    
    // xml summary
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    
    c.OperationFilter<SwaggerHeaderfilter>();

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

// register dbContext
builder.Services.AddDbContext<AppDbContext>();

// singleton, scoped, transient
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPersistence, DbPersistence>();
builder.Services.AddServices();
builder.Services.AddSingleton<EncryptUtils>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            ValidIssuer = "",
            ValidAudience = "",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role,
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.UseRouting();
app.UseAuthentication();
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
            
            if (exception?.Error is UnauthorizedException)
            {
                errorRespnose.Code = "E-002";
                errorRespnose.Status = "Failed";
                errorRespnose.Message = exception?.Error?.Message;

                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(errorRespnose);
                return;
            }
            
            if (exception?.Error is ConflictException)
            {
                errorRespnose.Code = "E-003";
                errorRespnose.Status = "Failed";
                errorRespnose.Message = exception?.Error?.Message;

                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(errorRespnose);
                return;
            }
            
            if (exception?.Error is BadRequestException)
            {
                errorRespnose.Code = "E-004";
                errorRespnose.Status = "Failed";
                errorRespnose.Message = exception?.Error.Message;

                context.Response.StatusCode = 400;
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