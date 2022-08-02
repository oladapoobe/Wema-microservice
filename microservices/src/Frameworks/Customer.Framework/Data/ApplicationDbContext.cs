namespace Customer.Framework.Data
{
    using Microsoft.EntityFrameworkCore;
    using Customer.Framework.Data.Entities;
    using Customer.Framework.Data.EntityConfigurations;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            LocalGovernmentConfiguration
                .Configure(modelBuilder.Entity<LocalGovernment>());
            CustomerEntityConfiguration
               .Configure(modelBuilder.Entity<Customer>());
            OtpLogConfiguration
              .Configure(modelBuilder.Entity<OtpLog>());
            StateConfiguration
              .Configure(modelBuilder.Entity<State>());

            base.OnModelCreating(modelBuilder);
        }

       


        DbSet<LocalGovernment> LocalGovernments { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<OtpLog> OtpLogs { get; set; }
        DbSet<State> States { get; set; }
    }
}
