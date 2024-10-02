namespace Domain.Products;

public interface IProductRepository
{
    Task<bool> AnyAsync(ModelCode modelCode, CancellationToken cancellationToken);

    Task<bool> AnyAsync(Barcode barcode, CancellationToken cancellationToken);

    Task CreateAsync(Product product, CancellationToken cancellationToken);

    Task UpdateAsync(Product product, CancellationToken cancellationToken);

    Task<Product> GetAsync(ProductId id, CancellationToken cancellationToken);

    Task<List<Product>> GetAllAsync(int skip, int take, CancellationToken cancellationToken);
}
