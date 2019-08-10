using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartaKata.Domain.Exceptions
{
    public class ItemException : Exception
    {
        public ItemException(string message) : base(message)
        {
        }
    }

    public static class ItemExceptionMessage
    {
        public const string ProductNotInformed = "Product not informed";
        public const string QuantityLessOrEqualsZero = "Quantity cannot be less than or equal to zero";
        public const string QuantityGreaterThanItems = "Quantity cannot be greater than quantity of items";
    }
}
