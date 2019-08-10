namespace ShoppingCartaKata.Domain
{
    public interface IItem
    {
        IProduct Product { get; }
        int Quantity { get; }
        ICartItemOffer Offer { get; }
        double TotalPrice { get; }

        IItem Add(int quantity);
        IItem Remove(int quantity);
    }
}
