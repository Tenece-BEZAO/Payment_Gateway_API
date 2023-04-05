using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Infrastructure.jwt;

namespace Payment_Gateway.BLL.Extentions
{
    public static class ConfigurationBinder
    {
        public static IServiceCollection BindConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            JwtConfig jwt = new();
            AppConstants appConstants = new();


            configuration.GetSection("JwtConfig").Bind(jwt);
            configuration.GetSection("AppConstants").Bind(appConstants);

            services.AddSingleton(jwt);
            services.AddSingleton(appConstants);


            return services;
        }
    }
}
