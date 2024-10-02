using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder
            .Property<ProductItemId>(_ => _.Id)
            .HasConversion(
                productItemId => productItemId.Value, 
                value => ProductItemId.Create(value).Data);

        builder
            .Property<ProductId>(_ => _.ProductId)
            .HasConversion(
                productId => productId.Value,
                value => ProductId.Create(value).Data);

        builder
            .Property<Barcode>(_ => _.Barcode)
            .HasConversion(
                barcode => barcode.Value,
                value => Barcode.Create(value).Data);

        builder
            .OwnsOne<Money>(_ => _.Price, priceBuilder =>
            {
                priceBuilder
                    .Property<string>(_ => _.Currency)
                    .HasMaxLength(3);

                priceBuilder
                    .Property<decimal>(_ => _.Amount);
            });
    }
}
