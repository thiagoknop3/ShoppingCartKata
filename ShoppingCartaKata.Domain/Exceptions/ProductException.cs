using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartaKata.Domain.Exceptions
{
    public class ProductException : Exception
    {
        public ProductException(string message) : base(message)
        {
        }
    }
    public static class ProductExceptionMessage
    {
        public const string IdLessOrEqualsZero = "Product Id cannot be less than or equal to zero";
        public const string PriceLessOrEqualsZero = "Product Price cannot be less than or equal to zero";
        public const string NameCannotBeEmpty = "Product Name cannot be empty";
    }
}
