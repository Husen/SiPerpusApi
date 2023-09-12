using SiPerpusApi.Services;

namespace SiPerpusApi.Services;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<ICategoryService, CategoryService>();
        service.AddScoped<IPublisherService, PublisherService>();
        service.AddScoped<IRackService, RackService>();
        service.AddScoped<IBookService, BookService>();
        service.AddScoped<IMemberService, MemberService>();
        service.AddScoped<ILoanService, LoanService>();
        service.AddScoped<IAuthService, AuthService>();
    }
}