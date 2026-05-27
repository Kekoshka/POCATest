using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using POCATest.Common.Exceptions;
using POCATest.Middlewares;
using System.Reflection;
using System.Text;
using РОСАTest.Common.DataSeed;
using РОСАTest.Common.Options;
using РОСАTest.Context;

namespace РОСАTest.Common.Extensions
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

                    if (context.Statuses.Any() ||
                        context.Roles.Any() ||
                        context.CertificateTypes.Any())
                        return;

                    context.CertificateTypes.AddRange(CertificateTypesSeed.CertificateTypes);
                    context.Roles.AddRange(RolesSeed.Roles);
                    context.Statuses.AddRange(StatusesSeed.Statuses);
                    context.SaveChanges();
                });
            });
        }

        public static void AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration
                .GetSection(nameof(JWTOptions))
                .Get<JWTOptions>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    };
                });

            services.AddAuthorization();
        }

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTOptions>(configuration.GetSection(nameof(JWTOptions)));
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
