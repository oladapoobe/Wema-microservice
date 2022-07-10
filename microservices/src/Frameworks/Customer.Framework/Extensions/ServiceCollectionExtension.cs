namespace Customer.Framework.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Customer.Framework.Data;
    using Customer.Framework.Data.Interface;
    using Customer.Framework.Services;
    using Customer.Framework.Services.Interface;
    using AutoMapper;
    using Customer.Framework.Domain.Models;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTransactionFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // Service
            services.AddScoped<ICustomerService, CustomerService>();
            services.Configure<SmsSettings>(c => configuration.GetSection("SmsSettings"));
            services.AddTransient<ISmsService, SmsService>();
            // Connection String
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

            return services;
        }
    }
}
