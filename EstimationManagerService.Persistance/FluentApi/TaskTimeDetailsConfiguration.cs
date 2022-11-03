using EstimationManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstimationManagerService.Persistance.FluentApi;

public class TaskTimeDetailsConfiguration : IEntityTypeConfiguration<TaskTimeDetails>
{
    public void Configure(EntityTypeBuilder<TaskTimeDetails> builder)
    {
        builder.HasOne(x => x.UserTask)
            .WithMany(x => x.TaskTimeDetails)
            .HasForeignKey(x => x.UserTaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}