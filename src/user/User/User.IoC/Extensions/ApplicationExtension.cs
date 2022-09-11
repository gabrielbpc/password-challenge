using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using User.Application.Commands.CreateAnUser;

namespace User.IoC.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationLayerDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetEntryAssembly());

            services.AddMediatR(typeof(CreateAnUserCommandHandler).Assembly);

            return services;
        }
    }
}
