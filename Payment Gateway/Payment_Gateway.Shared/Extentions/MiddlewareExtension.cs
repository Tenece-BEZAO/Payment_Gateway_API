using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Payment_Gateway.DAL.Context;
using Payment_Gateway.DAL.Implementation;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Shared.Handlers;
using Payment_Gateway.Shared.Implementation;
using Payment_Gateway.Shared.Infrastructure.jwt;
using Payment_Gateway.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.Extentions
{
    public static class MiddlewareExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IJWTAuthenticator, JwtAuthenticator>();
            services.AddTransient<IAuthorizationHandler, CustomAuthorizationHandler>();
            services.AddTransient<IUnitOfWork, UnitOfWork<PaymentGatewayDbContext>>();
            services.AddTransient<IServiceFactory, ServiceFactory>();
            //services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<Interfaces.IAuthenticationService, Implementation.AuthenticationService>();
            services.AddTransient<IRoleService, RoleService>();
            //services.AddTransient<IStaffService, StaffService>();
            //services.AddTransient<IToDoItemService, ToDoItemService>();
        }
    }
}
