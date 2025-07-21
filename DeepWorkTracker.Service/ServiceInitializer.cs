using DeepWorkTracker.Core.Contracts.Services;
using DeepWorkTracker.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeepWorkTracker.Service
{
    public class ServiceInitializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IDeepWorkService, DeepWorkService>();
        }
    }
}
