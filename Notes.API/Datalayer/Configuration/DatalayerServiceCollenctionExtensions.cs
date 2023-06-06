using Microsoft.EntityFrameworkCore;
using Notes.API.Datalayer.Context;

namespace Notes.API.Datalayer.Configuration
{
    public static class DatalayerServiceCollectionExtensions
    {
        public static IServiceCollection AddDatalayer(this IServiceCollection services)
        {

            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var datalayerConfiguration = new DatalayerConfiguration();
            configuration?.GetSection(nameof(DatalayerConfiguration)).Bind(datalayerConfiguration);

            services.AddDbContext<DatalayerContext>(options =>
            {
                options.UseSqlite(datalayerConfiguration.ConnectionString);
            });
            
            return services;
        }
    }
}
