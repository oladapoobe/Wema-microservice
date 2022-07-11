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
    using Customer.Framework.Mappers;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTransactionFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient( typeof(IHttpClientWrapperRespository<>), typeof(HttpClientWrapperRespository<>));
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            // Service
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILocalGovtService, LocalGovtService>();
            services.AddScoped<IOTPService, OTPService>();
            services.AddScoped<IStateService, StateService>();
    
            services.Configure<SmsSettings>(c => configuration.GetSection("SmsSettings"));
            services.AddTransient<ISmsService, SmsService>();
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            // Connection String
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

            return services;
        }
    }
}
