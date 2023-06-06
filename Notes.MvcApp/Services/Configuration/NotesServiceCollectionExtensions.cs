using Notes.MvcApp.Interfaces;
using Notes.MvcApp.Notes.Api.Configuration;

namespace Notes.MvcApp.Services.Configuration
{
    public static class NotesServiceCollectionExtensions
    {
        public static IServiceCollection AddNotesService(this IServiceCollection services)
        {
            services.AddScoped<INotesService, NotesService>();
            services.AddNotesApi();
            return services;
        }
    }
}
