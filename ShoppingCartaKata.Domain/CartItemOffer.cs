using ShoppingCartaKata.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartaKata.Domain
{
    public class CartItemOffer : ICartItemOffer
    {
        public CartItemOffer(IProduct product)
        {
            this.Product = product;
        }

        public ICartItemOffer Create(int quantity, double price)
        {
            this.Quantity = quantity;
            this.Price = price;
            return this;
        }

        private IProduct _product;
        [Required]
        public IProduct Product
        {
            get { return _product; }
            private set
            {
                if (value == null)
                {
                    throw new CartItemOfferException(CartItemOfferExceptionMessage.ProductOfferNotInformed);
                }
                _product = value;
            }
        }

        private int _quantity;
        [Required]
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value <= 0)
                {
                    throw new CartItemOfferException(CartItemOfferExceptionMessage.QuantityLessOrEqualsZero);
                }
                _quantity = value;
            }
        }

        private double _price;
        [Required]
        public double Price
        {
            get { return _price; }
            private set
            {
                if (value <= 0)
                {
                    throw new CartItemOfferException(CartItemOfferExceptionMessage.PriceLessOrEqualsZero);
                }
                _price = value;
            }
        }
    }
}
