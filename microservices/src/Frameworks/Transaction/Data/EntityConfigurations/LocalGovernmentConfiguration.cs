namespace Customer.Framework.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Customer.Framework.Data.Entities;

    public static class LocalGovernmentConfiguration
    {
        public static void Configure(EntityTypeBuilder<LocalGovernment> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(u => u.State_id).IsRequired(); 
            entityBuilder.Property(t => t.Name).IsRequired();
     
        }
    }
}
