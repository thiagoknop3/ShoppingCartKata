namespace ShoppingCartaKata.Domain
{
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        double Price { get; }
    }
}
