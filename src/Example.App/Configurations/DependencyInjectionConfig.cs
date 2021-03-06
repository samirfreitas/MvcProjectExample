using Example.App.Extensions;
using Example.Business.Intefaces;
using Example.Business.Notifications;
using Example.Business.Services;
using Example.Data.Context;
using Example.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace Example.App.Configurations
{
    public static class DependencyInjectionConfig 
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ExampleDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, CoinValidationAttributeAdapterProvider>();

            services.AddScoped<INotificator, Notificator>();
            services.AddScoped<IVendorServices, VendorServices>();
            services.AddScoped<IProductServices, ProductServices>();
            return services;
        }
    }

}
