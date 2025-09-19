namespace OnlineStore.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

public static class VariantAttributeValueSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VariantAttributeValue>().HasData(
            new VariantAttributeValue { Id = 1, ProductVariantId = 1, AttributeId = 1, AttributeValueId = 1 }, // Red
            new VariantAttributeValue { Id = 2, ProductVariantId = 1, AttributeId = 2, AttributeValueId = 5 }, // Small

            new VariantAttributeValue { Id = 3, ProductVariantId = 2, AttributeId = 1, AttributeValueId = 2 }, // Blue
            new VariantAttributeValue { Id = 4, ProductVariantId = 2, AttributeId = 2, AttributeValueId = 6 }, // Medium

            new VariantAttributeValue { Id = 5, ProductVariantId = 3, AttributeId = 3, AttributeValueId = 9 }, // 128GB
            new VariantAttributeValue { Id = 6, ProductVariantId = 3, AttributeId = 1, AttributeValueId = 1 }, // Red

            new VariantAttributeValue { Id = 7, ProductVariantId = 4, AttributeId = 2, AttributeValueId = 5 }, // Small

            new VariantAttributeValue { Id = 8, ProductVariantId = 5, AttributeId = 3, AttributeValueId = 10 }, // 256GB

            new VariantAttributeValue { Id = 9, ProductVariantId = 6, AttributeId = 1, AttributeValueId = 2 }, // Blue

            new VariantAttributeValue { Id = 10, ProductVariantId = 7, AttributeId = 2, AttributeValueId = 6 }, // Medium

            new VariantAttributeValue { Id = 11, ProductVariantId = 8, AttributeId = 3, AttributeValueId = 9 }, // 128GB

            new VariantAttributeValue { Id = 12, ProductVariantId = 9, AttributeId = 1, AttributeValueId = 1 }, // Red

            new VariantAttributeValue { Id = 13, ProductVariantId = 10, AttributeId = 2, AttributeValueId = 5 }  // Small
        );
    }
}
