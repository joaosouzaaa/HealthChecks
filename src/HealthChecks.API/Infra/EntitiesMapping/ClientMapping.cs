using HealthChecks.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthChecks.API.Infra.EntitiesMapping;

internal sealed class ClientMapping : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired(true)
               .HasColumnName("name")
               .HasColumnType("varchar(200)");

        builder.Property(c => c.Description)
               .IsRequired(true)
               .HasColumnName("description")
               .HasColumnType("varchar(2000)");

        builder.Property(c => c.IsActive)
               .IsRequired(true)
               .HasColumnName("is_active")
               .HasColumnType("boolean");
    }
}
