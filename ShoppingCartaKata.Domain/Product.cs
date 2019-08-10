using ShoppingCartaKata.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartaKata.Domain
{
    public class Product : IProduct
    {
        public IProduct Create(int id, string name, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;

            return this;
        }

        private int _id;
        [Required]
        public int Id
        {
            get { return _id; }
            private set
            {
                if (value <= 0)
                {
                    throw new ProductException(ProductExceptionMessage.IdLessOrEqualsZero);
                }
                _id = value;
            }
        }

        private string _name;
        [Required]
        public string Name
        {
            get { return _name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ProductException(ProductExceptionMessage.NameCannotBeEmpty);
                }
                _name = value;
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
                    throw new ProductException(ProductExceptionMessage.PriceLessOrEqualsZero);
                }
                _price = value;
            }
        }
    }
}
