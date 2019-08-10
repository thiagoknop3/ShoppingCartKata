using ShoppingCartaKata.Domain;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartKata
{
    public class Program
    {
        static readonly Container container;
        private static List<IProduct> Products;
        private static List<ICartItemOffer> Offers;
        private static ICart Cart;

        static Program()
        {

            container = new Container();

            container.Register<IProduct, Product>();
            container.Register<ICart, Cart>();
            container.Register<ICartItemOffer, CartItemOffer>();
            container.Register<IItem, Item>();

            container.Verify();
        }

        static void Main(string[] args)
        {
            AddProducts();
            AddOffers();
            Cart = new Cart().AddOffers(Offers);

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q))
            {
                Console.Clear();
                if (Cart.Items.Any())
                {
                    foreach (var item in Cart.Items)
                    {
                        Console.WriteLine("----------------------------");
                        Console.WriteLine(string.Format("{0} - {1} ({2})  TOTAL = {3}", item.Product.Id, item.Product.Name, item.Quantity, item.TotalPrice));
                        if (item.Offer != null)
                        {
                            Console.WriteLine(string.Format("Offer: quantity: {0} for: {1}", item.Offer.Quantity, item.Offer.Price));
                        }

                    }
                    Console.WriteLine("----------------------------");
                    Console.WriteLine((string.Format("TOTAL CART: {0}", Cart.Total)));
                    Console.WriteLine();
                }


                Console.WriteLine("Select a product (ESC to QUIT)");
                foreach (var product in Products)
                {
                    Console.WriteLine(product.Id + " - " + product.Name);
                }

                var selectedId = Console.ReadLine();
                if(selectedId.Trim().ToUpper() == ConsoleKey.Q.ToString())
                {
                    return;
                }
                int id;
                int.TryParse(selectedId, out id);
                var selectedProduct = Products.FirstOrDefault(p => p.Id == id);
                if (selectedProduct == null)
                {
                    Console.WriteLine("Invalid selected product");
                    continue;
                }


                var quantityString = "";
                int selectedQuantity;
                while (!int.TryParse(quantityString, out selectedQuantity))
                {
                    Console.WriteLine("Insert the quantity");
                    quantityString = Console.ReadLine();
                }
                Console.WriteLine();


                Cart.AddItem(selectedProduct, selectedQuantity);
            }

        }



        private static void AddProducts()
        {
            Products = new List<IProduct>();
            Products.Add(new Product().Create(1, "Apple", 30));
            Products.Add(new Product().Create(2, "Banana", 50));
            Products.Add(new Product().Create(3, "Peach", 60));
        }
        private static void AddOffers()
        {
            Offers = new List<ICartItemOffer>();
            Offers.Add(new CartItemOffer(Products.FirstOrDefault(p => p.Id == 1)).Create(2, 45D));
            Offers.Add(new CartItemOffer(Products.FirstOrDefault(p => p.Id == 2)).Create(3, 130D));

        }
    }
}
