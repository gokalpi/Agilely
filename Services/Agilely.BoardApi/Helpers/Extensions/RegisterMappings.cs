using Agilely.BoardApi.Contracts;
using Agilely.BoardApi.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agilely.BoardApi.Helpers.Extensions
{
    public class RegisterMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register Interface Mappings for Repositories
            services.AddTransient<IBoardManager, BoardManager>();
        }
    }
}