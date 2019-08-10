using System;

namespace ShoppingCartaKata.Domain.Exceptions
{
    public class CartException : Exception
    {
        public CartException(string message) : base(message)
        {
        }

        
    }
    public static class CartExceptionMessage
    {
        public const string ProductDoesntExist = "Product does not exist in cart";
    }
}
