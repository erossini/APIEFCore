using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APIEFCore.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public const string ConnectionName = "DefaultConnection";

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration,
            string cnnStringName = ConnectionName,
            string? cnnString = null)
        {
            if (cnnString == null)
                cnnString = configuration.GetConnectionString(cnnStringName);

            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(cnnString)
                .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
                .EnableDetailedErrors());

            return services;
        }
    }
}