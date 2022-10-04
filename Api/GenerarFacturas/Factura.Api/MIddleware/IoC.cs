using LayerAccess;
using LayerBusiness.Home;
using LayerBusiness.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViewModel.Security;

namespace Factura.Api.MIddleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependecy(this IServiceCollection services,IConfiguration configuration)
        {

            var settingToken = configuration.GetSection("TokenSetting").Get<TokenSetting>();
            services.AddSingleton(settingToken);

            services.AddDbContext<InvoiceContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceDetailService, InvoiceDetailService>();
            return services;
        }



        public static IServiceCollection AddCorsLocal(this IServiceCollection services)
        {

            services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();


            }));


            return services;
        }
    }
}
