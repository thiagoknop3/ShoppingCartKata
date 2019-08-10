using Moq;
using NUnit.Framework;
using ShoppingCartaKata.Domain;
using ShoppingCartaKata.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartKata.Tests
{
    [TestFixture]
    public class CartTests
    {

        Cart cart;
        [Test]
        public void Add_Offers_Success()
        {
            var offers = MockOffers();
            cart = new Cart();
            cart.AddOffers(offers.Object);

            Assert.AreEqual(offers.Object, cart.ActualOffers);
        }

        [Test]
        public void Add_Item_Success()
        {
            var product = MockBanana();
            int quantity = 2;
            cart = new Cart();
            cart.AddItem(product.Object, quantity);

            Assert.IsTrue(cart.Items.Any(i=>i.Product == product.Object && i.Quantity == quantity));
        }

        [Test]
        public void Remove_Item_Success()
        {
            var banana = MockBanana();
            int quantity = 2;
            int quantityToRemove = 1;
            cart = new Cart();
            cart.AddItem(banana.Object, quantity);
            cart.RemoveItem(banana.Object, quantityToRemove);

            Assert.IsTrue(cart.Items.Any(i => i.Product == banana.Object && i.Quantity == quantity- quantityToRemove));
        }



        [Test]
        public void Remove_Product_Doesnt_Exist()
        {
            var banana = MockBanana();
            var apple = MockApple();
            int quantity = 2;
            int quantityToRemove = 1;
            cart = new Cart();
            cart.AddItem(banana.Object, quantity);

            var ex = Assert.Throws<CartException>(() => cart.RemoveItem(apple.Object, quantityToRemove));
            Assert.AreEqual(CartExceptionMessage.ProductDoesntExist, ex.Message);
        }
        

        private Mock<List<ICartItemOffer>> MockOffers()
        {
            return new Mock<List<ICartItemOffer>>();
        }

        private Mock<IProduct> MockBanana()
        {
            var banana = new Mock<IProduct>(MockBehavior.Strict);
            banana.Setup(p => p.Id).Returns(2);
            banana.Setup(p => p.Name).Returns("Banana");
            banana.Setup(p => p.Price).Returns(50);
            return banana;
        }

        private Mock<IProduct> MockApple()
        {
            var apple = new Mock<IProduct>(MockBehavior.Strict);
            apple.Setup(p => p.Id).Returns(1);
            apple.Setup(p => p.Name).Returns("Apple");
            apple.Setup(p => p.Price).Returns(30);
            return apple;
        }
    }
}
