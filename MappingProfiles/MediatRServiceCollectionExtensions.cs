using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace MappingProfiles
{
    public static class MediatRServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRFromAssemblyContaining<T>(this IServiceCollection services)
        {
            var assembly = typeof(T).Assembly;
            var serviceTypes = assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract);
            services.AddMediatR(serviceTypes.ToArray());
            return services;
        }
    }
}
