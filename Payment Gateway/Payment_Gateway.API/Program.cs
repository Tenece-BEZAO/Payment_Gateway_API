using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using NLog;
using Payment_Gateway.API.Extensions;
using Payment_Gateway.API.Filter;
using Payment_Gateway.BLL.Extentions;
using Payment_Gateway.BLL.Implementation;
using Payment_Gateway.BLL.Implementation.Services;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.Paystack.Implementation;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.DAL.Context;
using Payment_Gateway.DAL.Implementation;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Extensions;
using System.Reflection;

namespace Payment_Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
            "/nlog.config"));


            // Add services to the container.
            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.BindConfigurations(builder.Configuration);
            builder.Services.ConfigureLoggerService();
           

            builder.Services.AddAuthentication();
            //builder.Services.ConfigureIdentity();

            builder.Services.ConfigureJWT(builder.Configuration);
            builder.Services.AddDatabaseConnection();
            //builder.Services.ConfigureSqlContext(builder.Configuration);

            builder.Services.AddScoped<ValidationFilterAttribute>();

            //builder.Services.AddHttpContextAccessor();
            builder.Services.AddAutoMapper(Assembly.Load("Payment_Gateway.DAL"));
            builder.Services.AddScoped<IMakePaymentService, MakePaymentService>();
            builder.Services.AddScoped<IAdminServices, AdminServices>();
            builder.Services.AddScoped<IAdminProfileServices, AdminProfileServices>();
            builder.Services.AddScoped<IPayoutService, PayoutService>();
            builder.Services.AddScoped<IPayoutServiceExtension, PayoutServiceExtension>();
            builder.Services.AddScoped<IWalletService, WalletService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment_Gateway_API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                            Array.Empty<string>()
                    },
                });
            });

            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork<PaymentGatewayDbContext>>();
            builder.Services.RegisterServices();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseCors("AllowAll");


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.MapControllers();

            app.Run();
        }
    }
}