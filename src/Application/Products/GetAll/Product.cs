namespace Application.Products.GetAll;

public class Product
{
    public Guid Id { get; set; }

    public string ModelCode { get; set; }

    public List<ProductItem> ProductItems { get; set; }
}
