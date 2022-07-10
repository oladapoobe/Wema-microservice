namespace Customer.Framework.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Customer.Framework.Data.Entities;

    public static class CustomerEntityConfiguration
    {
        public static void Configure(EntityTypeBuilder<Customer> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Email).IsRequired();
            entityBuilder.Property(t => t.Pasword).IsRequired();
            entityBuilder.Property(t => t.PhoneNumber).IsRequired();
            entityBuilder.Property(t => t.Lga).IsRequired();
            entityBuilder.Property(t => t.State).IsRequired();
        }
    }
}
