using Application.Common.Interfaces;
using Infrastructure.Persistent.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));
        services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));

        return services;
    }
}