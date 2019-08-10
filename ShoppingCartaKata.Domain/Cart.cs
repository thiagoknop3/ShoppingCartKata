using ShoppingCartaKata.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartaKata.Domain
{
    public class Cart : ICart
    {
        public List<IItem> Items { get; private set; }
        public List<ICartItemOffer> ActualOffers { get; private set; }

        public double Total
        {
            get
            {
                if (Items == null)
                {
                    return 0;
                }
                return Math.Round(Items.Sum(i => i.TotalPrice), 2);
            }
        }


        public Cart()
        {
            ActualOffers = new List<ICartItemOffer>();
            Items = new List<IItem>();
        }

        public ICart AddOffers(List<ICartItemOffer> offers)
        {
            this.ActualOffers = offers;
            return this;
        }

        public ICart AddItem(IProduct product, int quantity)
        {
            var existItem = Items.FirstOrDefault(i => i.Product == product);
            if (existItem != null)
            {
                existItem.Add(quantity);
                return this;
            }
            var productOffer = this.ActualOffers.FirstOrDefault(o => o.Product == product);
            this.Items.Add(new Item(product, productOffer).Add(quantity));
            return this;
        }

        public ICart RemoveItem(IProduct product, int quantity)
        {
            var existItem = Items.FirstOrDefault(i => i.Product == product);
            if (existItem == null)
            {
                throw new CartException(CartExceptionMessage.ProductDoesntExist);
            }
            existItem.Remove(quantity);
            return this;
        }
    }
}
