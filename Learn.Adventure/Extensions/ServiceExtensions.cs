using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Repository.Implementation;
using Learn.Adventure.Services.Abstractions;
using Learn.Adventure.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Learn.Adventure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddAdventureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            services.AddScoped<IAdventureService, AdventureService>();
            services.AddScoped<IOptionService, OptionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserJourneyService, UserJourneyService>();
        }
    }
}