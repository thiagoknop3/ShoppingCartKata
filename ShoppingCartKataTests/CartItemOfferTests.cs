using Moq;
using NUnit.Framework;
using ShoppingCartaKata.Domain;

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
            mockProduct = new Mock<IProduct>(MockBehavior.Strict);
            mockProduct.Setup(p => p.Id).Returns(1);
            mockProduct.Setup(p => p.Name).Returns("Banana");
            mockProduct.Setup(p => p.Price).Returns(30);

            cartItemOffer = new CartItemOffer(mockProduct.Object);
            var result = cartItemOffer.Create(1, 10);

            Assert.IsTrue(result.Price == 10);
            Assert.IsTrue(result.Quantity == 1);
            Assert.IsTrue(result.Product == mockProduct);
            mockProduct.VerifyAll();
        }
    }
}
