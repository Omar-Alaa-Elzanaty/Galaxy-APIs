using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Application.Interfaces.Repositories.Suppliers;
using Galaxy.Domain.Identity;
using Galaxy.Presistance.Context;
using Galaxy.Presistance.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharamcy.Presistance.Repositories;

namespace Galaxy.Presistance.Extention
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddPresistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration)
                    .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
                    .AddTransient<IUnitOfWork, UnitOfWork>()
                    .AddTransient<ISupplierRepository, SupplierRepository>()
                    .AddTransient<IStockRepository, StockRepository>();

            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<GalaxyDbContext>(options =>
               options.UseLazyLoadingProxies().UseSqlServer(connectionString,
                   builder =>
                   {
                       builder.MigrationsAssembly(typeof(GalaxyDbContext).Assembly.FullName);
                   }));
            // Identity configuration
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<GalaxyDbContext>()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultTokenProviders();


            return services;
        }
    }
}
