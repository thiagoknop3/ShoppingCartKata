using Moq;
using NUnit.Framework;
using ShoppingCartaKata.Domain;
using ShoppingCartaKata.Domain.Exceptions;

namespace ShoppingCartKata.Tests
{
    [TestFixture]
    public class CartItemOfferTests
    {
        Mock<IProduct> mockProduct;
        CartItemOffer cartItemOffer;
        [Test]
        public void Create_Success()
        {
            this.CreateMockProduct();

            cartItemOffer = new CartItemOffer(mockProduct.Object);
            var result = cartItemOffer.Create(1, 10);

            Assert.IsTrue(result.Price == 10);
            Assert.IsTrue(result.Quantity == 1);
            Assert.IsTrue(result.Product == mockProduct.Object);
        }

        [Test]
        public void Create_Without_Product()
        {
            var ex = Assert.Throws<CartItemOfferException>(() => new CartItemOffer(null));
            Assert.AreEqual(CartItemOfferExceptionMessage.ProductOfferNotInformed, ex.Message);
        }

        [Test]
        public void Price_Less_Or_Equals_Zero()
        {
            this.CreateMockProduct();

            cartItemOffer = new CartItemOffer(mockProduct.Object);

            var ex = Assert.Throws<CartItemOfferException>(() => cartItemOffer.Create(1,0));
            Assert.AreEqual(CartItemOfferExceptionMessage.PriceLessOrEqualsZero, ex.Message);
        }

        [Test]
        public void Quantity_Less_Or_Equals_Zero()
        {
            this.CreateMockProduct();

            cartItemOffer = new CartItemOffer(mockProduct.Object);

            var ex = Assert.Throws<CartItemOfferException>(() => cartItemOffer.Create(0, 1));
            Assert.AreEqual(CartItemOfferExceptionMessage.QuantityLessOrEqualsZero, ex.Message);
        }

        private void CreateMockProduct()
        {
            mockProduct = new Mock<IProduct>(MockBehavior.Strict);
            mockProduct.Setup(p => p.Id).Returns(1);
            mockProduct.Setup(p => p.Name).Returns("Banana");
            mockProduct.Setup(p => p.Price).Returns(30);
        }

    }
}
