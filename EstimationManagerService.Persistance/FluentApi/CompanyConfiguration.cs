using System;
using EstimationManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstimationManagerService.Persistance.FluentApi;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasMany(x => x.Users)
                .WithMany(x => x.Companies);

        builder.HasOne(x => x.Admin)
                .WithMany(x => x.OwnCompanies)
                .HasForeignKey(x => x.AdminId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
