namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;

public class ReviewAttachmentConfiguration : IEntityTypeConfiguration<ReviewAttachment>
{
    public void Configure(EntityTypeBuilder<ReviewAttachment> builder)
    {
        builder.ToTable("ReviewAttachments");

        builder.HasKey(ra => ra.Id);

        builder.Property(ra => ra.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(ra => ra.UploadedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(ra => ra.Review)
            .WithMany(r => r.Attachments)
            .HasForeignKey(ra => ra.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
