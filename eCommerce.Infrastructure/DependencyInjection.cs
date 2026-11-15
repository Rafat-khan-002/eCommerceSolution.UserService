using eCommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
namespace eCommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //TO DO: Add services to the IoC container
            //Infrastructure services often include data access, caching and other low-level
            //components.

            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddTransient<DapperDbContext>();

            return services;
        }
    }
}