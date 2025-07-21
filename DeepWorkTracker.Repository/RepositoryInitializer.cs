using DeepWorkTracker.Core.Contracts.Repositories;
using DeepWorkTracker.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DeepWorkTracker.Repository
{
    public class RepositoryInitializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IDeepWorkRepository, DeepWorkRepository>();
        }
    }
}
