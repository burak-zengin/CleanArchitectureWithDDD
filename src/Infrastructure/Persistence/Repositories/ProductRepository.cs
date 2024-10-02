using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository(ProductDbContext context) : IProductRepository
{
    public async Task<bool> AnyAsync(ModelCode modelCode, CancellationToken cancellationToken)
    {
        return await context.Products.AnyAsync(_ => _.ModelCode == modelCode, cancellationToken);
    }

    public async Task<bool> AnyAsync(Barcode barcode, CancellationToken cancellationToken)
    {
        return await context.ProductItems.AnyAsync(_ => _.Barcode == barcode, cancellationToken);
    }

    public async Task CreateAsync(Product product, CancellationToken cancellationToken)
    {
        context.Add(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(int skip, int take, CancellationToken cancellationToken)
    {
        return await context.Products.Include(_ => _.ProductItems).AsNoTracking().Skip(skip).Take(take).ToListAsync(cancellationToken);
    }

    public async Task<Product> GetAsync(ProductId id, CancellationToken cancellationToken)
    {
        return await context.Products.Include(_ => _.ProductItems).AsNoTracking().FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        context.Update(product);
        await context.SaveChangesAsync(cancellationToken);
    }
}
