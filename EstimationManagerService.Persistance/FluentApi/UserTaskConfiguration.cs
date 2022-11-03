using System;
using EstimationManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstimationManagerService.Persistance.FluentApi;

public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
{
    public void Configure(EntityTypeBuilder<UserTask> builder)
    {
        builder.HasOne(x => x.Project)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
                .WithMany(x => x.UserTasks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.TaskTimeDetails)
            .WithOne(x => x.UserTask)
            .HasForeignKey(x => x.UserTaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
