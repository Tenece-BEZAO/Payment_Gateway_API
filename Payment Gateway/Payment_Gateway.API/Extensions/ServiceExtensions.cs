using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.BLL.LoggerService.Interface;
using Payment_Gateway.DAL.Context;
using Payment_Gateway.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.BLL.Implementation;
using Payment_Gateway.BLL.Infrastructure.jwt;
using Payment_Gateway.BLL.Handlers;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.DAL.Implementation;
using Microsoft.AspNetCore.Authorization;

namespace Payment_Gateway.API.Extensions
{

    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IJWTAuthenticator, JwtAuthenticator>();
            services.AddTransient<IAuthorizationHandler, CustomAuthorizationHandler>();
            services.AddTransient<IUnitOfWork, UnitOfWork<PaymentGatewayDbContext>>();
            services.AddTransient<IServiceFactory, ServiceFactory>();
            //services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ITransactionService, TransactionService>();
        }

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


        public static void AddDatabaseConnection(this IServiceCollection services)
        {
            IConfiguration? config;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                config = serviceProvider.GetService<IConfiguration>();
            }
            services.AddDbContext<PaymentGatewayDbContext>(options => options.UseSqlServer(config.GetConnectionString("sqlConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
           .AddDefaultTokenProviders()
           .AddEntityFrameworkStores<PaymentGatewayDbContext>();

        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<PaymentGatewayDbContext>()
            .AddDefaultTokenProviders();
        }
       

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }


      

    }
}
