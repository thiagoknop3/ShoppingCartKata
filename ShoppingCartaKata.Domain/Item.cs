using ShoppingCartaKata.Domain.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartaKata.Domain
{
    public class Item : IItem
    {
        
        private IProduct _product;
        [Required]
        public IProduct Product
        {
            get { return _product; }
            private set
            {
                if (value == null)
                {
                    throw new ItemException(ItemExceptionMessage.ProductNotInformed);
                }
                _product = value;
            }
        }


        [Required]
        public int Quantity { get; private set; }

        public ICartItemOffer Offer { get; private set; }

        [Required]
        public double TotalPrice { get; private set; }


        public Item(IProduct product, ICartItemOffer offer = null)
        {
            this.Product = product;
            this.Offer = offer;
        }

        public IItem Add(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ItemException(ItemExceptionMessage.QuantityLessOrEqualsZero);
            }
            this.Quantity += quantity;
            this.CalculateTotalPrice();
            return this;
        }

        public IItem Remove(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ItemException(ItemExceptionMessage.QuantityLessOrEqualsZero);
            }
            if (quantity > this.Quantity)
            {
                throw new ItemException(ItemExceptionMessage.QuantityGreaterThanItems);
            }
            this.Quantity -= quantity;
            this.CalculateTotalPrice();
            return this;
        }

        private void CalculateTotalPrice()
        {
            if (this.Offer == null)
            {
                this.TotalPrice = Math.Round(this.Product.Price * this.Quantity, 2);
                return;
            }
            int quantityOffers = (int)Math.Floor((decimal)this.Quantity / this.Offer.Quantity);
            int quantity = this.Quantity - (quantityOffers*this.Offer.Quantity);
            this.TotalPrice = Math.Round(this.Product.Price * quantity, 2) +
                              Math.Round(this.Offer.Price * quantityOffers, 2);

        }

    }
}
