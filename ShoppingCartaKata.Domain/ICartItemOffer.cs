namespace ShoppingCartaKata.Domain
{
    public interface ICartItemOffer
    {
        IProduct Product { get; }
        int Quantity { get; }
        double Price { get; }
        ICartItemOffer Create(int quantity, double price);
    }
}
