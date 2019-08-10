using System.Collections.Generic;

namespace ShoppingCartaKata.Domain
{
    public interface ICart
    {
        List<IItem> Items { get; }
        double Total { get; }

        ICart AddItem(IProduct product, int quantity);
        ICart RemoveItem(IProduct product, int quantity);
        ICart AddOffers(List<ICartItemOffer> offers);
    }
}
