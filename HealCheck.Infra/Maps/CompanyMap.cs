using HealhCheck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealCheck.Infra.Maps
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Document).HasMaxLength(100).HasColumnType("varchar");
            builder.Property(x => x.Name).HasMaxLength(20).HasColumnType("varchar");
        }
    }
}