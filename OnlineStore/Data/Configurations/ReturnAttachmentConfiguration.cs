namespace OnlineStore.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models;
public class ReturnAttachmentConfiguration : IEntityTypeConfiguration<ReturnAttachment>
{
    public void Configure(EntityTypeBuilder<ReturnAttachment> builder)
    {
        /* 
        Fields : 
        int Id  
        int ReturnItemId  
        ReturnItem ReturnItem  
        string FileName  
        DateTime UploadedAt  
        */

        // Table name (optional)
        builder.ToTable("ReturnAttachements");

        builder.HasKey(ra => ra.Id);
        builder.Property(ra => ra.FileName).IsRequired().HasMaxLength(100);
        builder.Property(ra => ra.UploadedAt).IsRequired();
        builder.HasOne(ra => ra.ReturnItem)
               .WithMany(ri => ri.Attachments)
               .HasForeignKey(ra => ra.ReturnItemId)
               .OnDelete(DeleteBehavior.Cascade)
               ;
    }
}