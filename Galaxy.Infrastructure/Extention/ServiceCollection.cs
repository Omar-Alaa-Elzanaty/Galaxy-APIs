using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Pharamcy.Infrastructure.Services.Localization;

namespace Galaxy.Infrastructure.Extention
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddLocalization()
                    .AddCollections();

            return services;
        }

        private static IServiceCollection AddLocalization(this IServiceCollection services)
        {
            LocalizationServiceCollectionExtensions.AddLocalization(services);


            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

            services.AddMvc().AddDataAnnotationsLocalization(op =>
            {
                op.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(JsonStringLocalizerFactory));
            }
             );

            services.Configure<RequestLocalizationOptions>(op =>
            {
                var supportedCultures = new[] {
                  new CultureInfo("de"),
                  new CultureInfo("ar"),
                 };
                op.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0]);

                op.SupportedCultures = supportedCultures;
            });

            return services;
        }
        private static IServiceCollection AddCollections(this IServiceCollection services)
        {
            //services.AddTransient<IAuthServices, AuthServices>();
            //services.AddTransient<IMediaService, MediaServices>();
            return services;
        }
    }
}
