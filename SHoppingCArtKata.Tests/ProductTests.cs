using NUnit.Framework;
using ShoppingCartaKata.Domain;
using ShoppingCartaKata.Domain.Exceptions;

namespace ShoppingCartKata.Tests
{
    [TestFixture]
    public class ProductTests
    {
        IProduct product;
        [Test]
        [TestCase(1, "Banana", 10)]
        public void Create_Success(int id, string name, double price)
        {

            product = new Product().Create(id, name, price);

            Assert.AreEqual(price,product.Price);
            Assert.AreEqual(id,product.Id);
            Assert.AreEqual(name, product.Name);
        }

        [Test]
        [TestCase(0, "Banana", 10)]
        public void Id_Less_Or_Equals_Zero(int id, string name, double price)
        {
            var ex = Assert.Throws<ProductException>(() => new Product().Create(id, name, price));
            Assert.AreEqual(ProductExceptionMessage.IdLessOrEqualsZero, ex.Message);
        }

        [Test]
        [TestCase(1, "Banana", 0)]
        public void Price_Less_Or_Equals_Zero(int id, string name, double price)
        {
            var ex = Assert.Throws<ProductException>(() => new Product().Create(id, name, price));
            Assert.AreEqual(ProductExceptionMessage.PriceLessOrEqualsZero, ex.Message);
        }

        [Test]
        [TestCase(1, "", 10)]
        public void Name_Cannot_Be_Empty(int id, string name, double price)
        {
            var ex = Assert.Throws<ProductException>(() => new Product().Create(id, name, price));
            Assert.AreEqual(ProductExceptionMessage.NameCannotBeEmpty, ex.Message);
        }
    }
}
