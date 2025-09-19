namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class FailedTaskConfiguration : IEntityTypeConfiguration<FailedTask>
{
    public void Configure(EntityTypeBuilder<FailedTask> builder)
    {
        // Table name (optional)
        builder.ToTable("FailedTasks");
        builder.HasKey(a => a.Id);
    }
}