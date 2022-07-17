using System;
using EstimationManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstimationManagerService.Persistance.FluentApi;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(x => x.UserTasks)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Companies)
                .WithMany(x => x.Users);

        builder.HasMany(x => x.OwnCompanies)
                .WithOne(x => x.Admin)
                .HasForeignKey(x => x.AdminId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

    }
}
