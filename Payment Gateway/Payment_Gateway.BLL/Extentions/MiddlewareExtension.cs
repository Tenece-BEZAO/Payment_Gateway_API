using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Payment_Gateway.DAL.Context;
using Payment_Gateway.DAL.Implementation;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.BLL.Handlers;
using Payment_Gateway.BLL.Implementation;
using Payment_Gateway.BLL.Infrastructure.jwt;
using Payment_Gateway.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Extentions
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
