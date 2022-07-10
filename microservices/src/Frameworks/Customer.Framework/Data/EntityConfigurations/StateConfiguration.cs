namespace Customer.Framework.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Customer.Framework.Data.Entities;

    public static class StateConfiguration
    {
        public static void Configure(EntityTypeBuilder<State> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
      
        }
    }
}
