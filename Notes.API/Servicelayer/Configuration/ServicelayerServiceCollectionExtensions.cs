using Notes.API.Common.Interfaces.Servicelayer;
using Notes.API.Servicelayer.Automapper.Profiles;
using Notes.API.Servicelayer.Services;

namespace Notes.API.Servicelayer.Configuration
{
    public static class ServicelayerServiceCollectionExtensions
    {
        public static IServiceCollection AddServicelayer(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IDatalayerService, DatalayerService>();
            return services;
        }
    }
}
