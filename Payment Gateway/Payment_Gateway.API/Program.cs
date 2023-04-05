using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using NLog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Payment_Gateway.API.Extensions;
using Payment_Gateway.BLL.Extentions;
using Payment_Gateway.BLL.Infrastructure.jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Payment_Gateway.BLL.Infrastructure;
using System.Reflection;
using Payment_Gateway.DAL.Context;
using Payment_Gateway.BLL.Handlers;

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

            builder.Services.AddControllers().AddNewtonsoftJson();

            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureSqlContext(builder.Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment Gateway", Version = "v1" });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer myold \""
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
            builder.Services.AddDatabaseConnection();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(jwt =>
                    {

                        JwtConfig jwtConfig = builder.Configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();
                        var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

                        jwt.SaveToken = true;
                        jwt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            RequireExpirationTime = true,
                            ValidIssuer = jwtConfig.Issuer,
                            ValidAudience = jwtConfig.Audience,
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            builder.Services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Authorization", policy => policy.Requirements.Add(new AuthorizationRequirment()));
            });

            builder.Services.BindConfigurations(builder.Configuration);

            //Add automapper
            builder.Services.AddAutoMapper(Assembly.Load("Payment_Gateway.BLL"));
            builder.Services.RegisterServices();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();
            var context = app.Services.CreateScope().ServiceProvider.GetService<PaymentGatewayDbContext>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.ConfigureException(builder.Environment);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.Run();
        }
    }
}