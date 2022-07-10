namespace Customer.Framework.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Customer.Framework.Data.Entities;

    public static class OtpLogConfiguration
    {
        public static void Configure(EntityTypeBuilder<OtpLog> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.PhoneNumber).IsRequired();
            entityBuilder.Property(t => t.Otp).IsRequired();
            entityBuilder.Property(t => t.DateCreated).IsRequired();
            entityBuilder.Property(t => t.DateExpired);

        }
    }
}


