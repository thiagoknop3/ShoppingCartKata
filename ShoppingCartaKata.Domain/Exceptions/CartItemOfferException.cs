using System;

namespace ShoppingCartaKata.Domain.Exceptions
{
    public class CartItemOfferException : Exception
    {
        public CartItemOfferException(string message) : base(message)
        {
        }
    }
    public static class CartItemOfferExceptionMessage
    {
        public const string QuantityLessOrEqualsZero = "Offer quantity cannot be less than or equal to zero";
        public const string PriceLessOrEqualsZero = "Offer Price cannot be less than or equal to zero";
        public const string ProductOfferNotInformed = "Product offer not informed";
    }
}
