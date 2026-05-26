using Microsoft.EntityFrameworkCore;
using POCATest.Common.Exceptions;
using POCATest.Middlewares;
using System.Reflection;
using РОСАTest.Context;
using РОСАTest.DataSeed;

namespace РОСАTest.Extensions
{
    public static class ServicesExtensions
    {
        static string ConfigNameConnectionStringPostgre = "PostgreSql";

        public static void UsePostgreSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var connectionString = configuration.GetConnectionString(ConfigNameConnectionStringPostgre);
                if (connectionString is null)
                    throw new NotFoundException($"Connection string with name {ConfigNameConnectionStringPostgre} not found");
                options.UseNpgsql(connectionString);
                options.UseSeeding((dbContext, _) =>
                {
                    var context = (AppDbContext)dbContext;

                    if (context.Statuses.Any())
                        return;

                    context.Statuses.AddRange(StatusesSeed.Statuses);
                    context.SaveChanges();
                });
            });
        }

        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        public static void RegisterExecutingAsseblyServices(this IServiceCollection services)
        {
            var serviceTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(st => st.IsClass && !st.IsAbstract && st.Name.EndsWith("Service"));
            foreach (var serviceType in serviceTypes)
            {
                var interfaceType = serviceType.GetInterfaces()
                    .FirstOrDefault(it => it.Name == $"I{serviceType.Name}");
                if (interfaceType is not null)
                    services.AddScoped(interfaceType, serviceType);
            }
        }

    }
}
