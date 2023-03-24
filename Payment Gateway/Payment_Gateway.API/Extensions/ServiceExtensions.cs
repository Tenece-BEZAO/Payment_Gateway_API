using Microsoft.EntityFrameworkCore;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.BLL.LoggerService.Interface;
using Payment_Gateway.DAL.Context;

namespace Payment_Gateway.API.Extensions
{
    public static class ServiceExtensions
    {
        //Allows all requests from all origins to be sent to our API
        public static void ConfigureCors(this IServiceCollection services) =>
             services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy", builder =>
                 builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
             });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
             services.Configure<IISOptions>(options =>
             {
             });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
           services.AddDbContext<PaymentGatewayDbContext>(opts =>
           opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    }
}
