using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property<ProductId>(_ => _.Id)
            .HasConversion(
                productId => productId.Value, 
                value => ProductId.Create(value).Data);

        builder
            .Property<ModelCode>(_ => _.ModelCode)
            .HasConversion(
                modelCode => modelCode.Value,
                value => ModelCode.Create(value).Data);

        builder
            .HasMany(_ => _.ProductItems)
            .WithOne()
            .HasForeignKey(_ => _.ProductId);
    }
}
