namespace Application.Products.GetAll;

public class ProductItem
{
    public Guid Id { get; set; }

    public string Barcode { get; set; }

    public string Currency { get; set; }

    public string Color { get; init; }

    public string Size { get; init; }

    public decimal Price { get; set; }
}
