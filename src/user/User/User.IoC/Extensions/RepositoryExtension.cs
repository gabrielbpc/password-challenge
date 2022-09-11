using Microsoft.Extensions.DependencyInjection;
using User.Domain.Contracts.Repository;
using User.Repository.Persistence;

namespace User.IoC.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositoryLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
