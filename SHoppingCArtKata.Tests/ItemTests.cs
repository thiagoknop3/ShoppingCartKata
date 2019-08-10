using Moq;
using NUnit.Framework;
using ShoppingCartaKata.Domain;
using ShoppingCartaKata.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ShoppingCartKata.Tests
{
    [TestFixture]
    public class ItemTests
    {
        Mock<IProduct> apple;
        Mock<IProduct> banana;
        Mock<IProduct> peach;
        Mock<ICartItemOffer> appleOffer;
        Item item;


        [Test]
        public void Create_Success()
        {
            this.CreateMockProducts();

            item = new Item(banana.Object);

            Assert.AreEqual(banana.Object, item.Product);
            Assert.AreEqual(0, item.TotalPrice);
            Assert.AreNotEqual(apple.Object, item.Product);
            Assert.AreNotEqual(peach.Object, item.Product);
            Assert.IsNull(item.Offer);
        }

        [Test]
        public void Create_Without_Product()
        {
            var ex = Assert.Throws<ItemException>(() => new Item(null));
            Assert.AreEqual(ItemExceptionMessage.ProductNotInformed, ex.Message);
        }

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        public void Add_Success(int quantity)
        {
            this.CreateMockProducts();

            item = new Item(apple.Object);
            item.Add(quantity);

            Assert.AreEqual(apple.Object, item.Product);
            Assert.AreEqual(quantity, item.Quantity);
            Assert.AreEqual(quantity * apple.Object.Price, item.TotalPrice);
            Assert.IsNull(item.Offer);
        }

        [Test]
        [TestCase(new int[] { 1, 21, 22 })]
        public void Add_More_Than_One_Success(int[] quantity)
        {
            this.CreateMockProducts();
            item = new Item(apple.Object);
            int sum = 0;
            foreach (var qtd in quantity)
            {
                sum += qtd;
                item.Add(qtd);
            }

            Assert.AreEqual(sum, item.Quantity);
            Assert.AreEqual(sum * apple.Object.Price, item.TotalPrice);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        public void Add_With_Quantity_Zero_Or_Less(int quantity)
        {
            this.CreateMockProducts();

            item = new Item(apple.Object);

            var ex = Assert.Throws<ItemException>(() => item.Add(quantity));
            Assert.AreEqual(ItemExceptionMessage.QuantityLessOrEqualsZero, ex.Message);
        }


        [Test]
        [TestCase(1,1)]
        [TestCase(100, 34)]
        public void Remove_Success(int quantity, int quantityToRemove)
        {
            this.CreateMockProducts();

            item = new Item(apple.Object);
            item.Add(quantity);
            item.Remove(quantityToRemove);

            Assert.AreEqual(quantity-quantityToRemove, item.Quantity);
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(100, -34)]
        public void Remove_With_Quantity_Zero_Or_Less(int quantity, int quantityToRemove)
        {
            this.CreateMockProducts();

            item = new Item(apple.Object);
            item.Add(quantity);

            var ex = Assert.Throws<ItemException>(() => item.Remove(quantityToRemove));
            Assert.AreEqual(ItemExceptionMessage.QuantityLessOrEqualsZero, ex.Message);
        }

        [Test]
        [TestCase(1, 3)]
        [TestCase(100, 1000)]
        public void Remove_Quantity_Greater_Than_Items(int quantity, int quantityToRemove)
        {
            this.CreateMockProducts();

            item = new Item(apple.Object);
            item.Add(quantity);

            var ex = Assert.Throws<ItemException>(() => item.Remove(quantityToRemove));
            Assert.AreEqual(ItemExceptionMessage.QuantityGreaterThanItems, ex.Message);
        }


        [Test]
        [TestCase(10)]
        public void Total_Price_With_Offer(int qty)
        {
            var apple = this.MockApple();
            var offer = this.MockOfferApple();


            item = new Item(apple.Object,offer.Object);
            item.Add(qty);

            int quantityOffers = (int)Math.Floor((decimal)item.Quantity / item.Offer.Quantity);
            int quantity = item.Quantity - (quantityOffers * item.Offer.Quantity);
            double totalPrice = Math.Round(item.Product.Price * quantity, 2) +
                              Math.Round(item.Offer.Price * quantityOffers, 2);


            Assert.AreEqual(totalPrice, item.TotalPrice);

        }


        private void CreateMockProducts()
        {
            MockApple();
            MockBanana();
            MockPeach();
        }

        private Mock<IProduct> MockApple()
        {
            apple = new Mock<IProduct>(MockBehavior.Strict);
            apple.Setup(p => p.Id).Returns(1);
            apple.Setup(p => p.Name).Returns("Apple");
            apple.Setup(p => p.Price).Returns(30);
            return apple;
        }

        private Mock<ICartItemOffer> MockOfferApple()
        {
            appleOffer = new Mock<ICartItemOffer>(MockBehavior.Strict);
            appleOffer.Setup(o => o.Product).Returns(apple.Object);
            appleOffer.Setup(o => o.Quantity).Returns(2);
            appleOffer.Setup(o => o.Price).Returns(45);
            return appleOffer;
        }

        private Mock<IProduct> MockBanana()
        {
            banana = new Mock<IProduct>(MockBehavior.Strict);
            banana.Setup(p => p.Id).Returns(2);
            banana.Setup(p => p.Name).Returns("Banana");
            banana.Setup(p => p.Price).Returns(50);
            return banana;
        }

        private Mock<IProduct> MockPeach()
        {
            peach = new Mock<IProduct>(MockBehavior.Strict);
            peach.Setup(p => p.Id).Returns(3);
            peach.Setup(p => p.Name).Returns("Peach");
            peach.Setup(p => p.Price).Returns(60);
            return peach;
        }

    }
}
