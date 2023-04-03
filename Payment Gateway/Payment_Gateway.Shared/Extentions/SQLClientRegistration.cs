using Payment_Gateway.DAL.Context;
using Payment_Gateway.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Payment_Gateway.Shared.Extentions
{
    public static class SqlClientRegistrationExtension
    {
        public static void AddDatabaseConnection(this IServiceCollection services)
        {
            IConfiguration? config;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                config = serviceProvider.GetService<IConfiguration>();
            }
            string cc = config.GetConnectionString("sqlConnection");
            services.AddDbContext<PaymentGatewayDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
           .AddDefaultTokenProviders()
           .AddEntityFrameworkStores<PaymentGatewayDbContext>();

        }
    }
}
