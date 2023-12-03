using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleShop {
    class ShoppingCart<T> where T : ShopItem {
        private List<T> items = new List<T>();

        public void AddToCart(T item) => items.Add(item);
        public bool IsEmpty() => items.Count == 0;

        public void ViewCart() {
            //Console.WriteLine($"Shopping Cart Contents ({typeof(T).Name}s):");
            Echo.Print("Shopping Cart Contents:", ConsoleColor.Cyan);
            items.ForEach(Console.WriteLine);
            CalculateTotal();
        }

        public void CalculateTotal() {
            var subTotal = items.Sum(item => item.Price);
            Echo.Print("===========================================================", ConsoleColor.Cyan);
            Echo.Print($"-------------------------Total----------------------------", ConsoleColor.Cyan);
            Echo.Print("===========================================================", ConsoleColor.Cyan);
            Echo.Print($"Sub Total: ${subTotal}", ConsoleColor.Cyan);
            Echo.Print($"WAT: 100", ConsoleColor.Cyan);
            Echo.Print($"Total: ${subTotal + 100}", ConsoleColor.Cyan);
            //return 0;
        }


        public void ClearCart() => items.Clear();


    }

}
