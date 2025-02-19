using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace APIEFCore.Mapper
{
    public static class MapperRegistration
    {
        public static IServiceCollection AddMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}