using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleShop {
    class GorsiShop {
        private List<Laptop> laptops = new List<Laptop>();
        private List<Accessory> accessories = new List<Accessory>();
        private ShoppingCart<ShopItem> cart = new ShoppingCart<ShopItem>();
        private Stack<Action> menuHistory = new Stack<Action>();
        public void Open() {
            var counter = 0;
            for (int i = 0; i < 30; i++) {
                Console.Clear();

                switch (counter % 4) {
                    case 0: {
                            Console.WriteLine("╔════╤╤╤╤════╗");
                            Console.WriteLine("║    │││ \\  ║");
                            Console.WriteLine("║    │││  O  ║");
                            Console.WriteLine("║    OOO     ║");
                            break;
                        };
                    case 1: {
                            Console.WriteLine("╔════╤╤╤╤════╗");
                            Console.WriteLine("║    ││││    ║");
                            Console.WriteLine("║    ││││    ║");
                            Console.WriteLine("║    OOOO    ║");
                            break;
                        };
                    case 2: {
                            Console.WriteLine("╔════╤╤╤╤════╗");
                            Console.WriteLine("║   / │││    ║");
                            Console.WriteLine("║  O  │││    ║");
                            Console.WriteLine("║     OOO    ║");
                            break;
                        };
                    case 3: {
                            Console.WriteLine("╔════╤╤╤╤════╗");
                            Console.WriteLine("║    ││││    ║");
                            Console.WriteLine("║    ││││    ║");
                            Console.WriteLine("║    OOOO    ║");
                            break;
                        };
                }

                counter++;
                Thread.Sleep(200);
            }
            AddShopItems();
            menuHistory.Push(DisplayMainMenu);
            while (true) {
                var currentMenu = menuHistory.Peek();
                currentMenu.Invoke();
            }
        }

        private void DisplayMainMenu() {
            Console.Clear();


            Echo.Print("\r\n\r\n  ________                    .__    _________.__                   \r\n /  _____/  ___________  _____|__|  /   _____/|  |__   ____ ______  \r\n/   \\  ___ /  _ \\_  __ \\/  ___/  |  \\_____  \\ |  |  \\ /  _ \\\\____ \\ \r\n\\    \\_\\  (  <_> )  | \\/\\___ \\|  |  /        \\|   Y  (  <_> )  |_> >\r\n \\______  /\\____/|__|  /____  >__| /_______  /|___|  /\\____/|   __/ \r\n        \\/                  \\/             \\/      \\/       |__|    \r\n\r\n", ConsoleColor.Magenta);



            Echo.Print("-----------------------Main Menu:-------------------------", ConsoleColor.Cyan);
            //  ________                    .__    _________.__                   
            // /  _____/  ___________  _____|__|  /   _____/|  |__   ____ ______  
            ///   \  ___ /  _ \_  __ \/  ___/  |  \_____  \ |  |  \ /  _ \\____ \ 
            //\    \_\  (  <_> )  | \/\___ \|  |  /        \|   Y  (  <_> )  |_> >
            // \______  /\____/|__|  /____  >__| /_______  /|___|  /\____/|   __/ 
            //        \/                  \/             \/      \/       |__|    




            Console.WriteLine("1. List Available Laptops");
            Console.WriteLine("2. List Available Accessories");
            Console.WriteLine("3. Cart & Checkout");
            Echo.Print("4. Exit the app", ConsoleColor.Magenta);
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine().ToLower();

            switch (choice) {

                case "1":
                    menuHistory.Push(() => ListAvailableItems<Laptop>(laptops, "Laptops"));
                    break;
                case "2":
                    menuHistory.Push(() => ListAvailableItems<Accessory>(accessories, "Accessories"));
                    break;
                case "3":
                    menuHistory.Push(() => Checkout());
                    break;
                case "4":
                    Echo.Print("\r\n\r\n___________.__                   __     _____.___.            ._.\r\n\\__    ___/|  |__ _____    ____ |  | __ \\__  |   | ____  __ __| |\r\n  |    |   |  |  \\\\__  \\  /    \\|  |/ /  /   |   |/  _ \\|  |  \\ |\r\n  |    |   |   Y  \\/ __ \\|   |  \\    <   \\____   (  <_> )  |  /\\|\r\n  |____|   |___|  (____  /___|  /__|_ \\  / ______|\\____/|____/ __\r\n                \\/     \\/     \\/     \\/  \\/                    \\/\r\n_______________.___.___________                                  \r\n\\______   \\__  |   |\\_   _____/                                  \r\n |    |  _//   |   | |    __)_                                   \r\n |    |   \\\\____   | |        \\                                  \r\n |______  // ______|/_______  /                                  \r\n        \\/ \\/               \\/                                   \r\n\r\n", ConsoleColor.Magenta);
                    Thread.Sleep(4000);
                    menuHistory.Pop();
                    Environment.Exit(0);
                    break;
                default:
                    Echo.Print("Invalid choice. Please try again.", ConsoleColor.Red);
                    break;
            }
        }

        private void ListAvailableItems<T>(List<T> items, string category) where T : ShopItem {
            Console.Clear();
            Echo.Print($"-----------------------{category} Menu:-------------------------", ConsoleColor.Cyan);
            Console.WriteLine($"Available {category}:");


            // Group items by brand
            var groupedItems = items.GroupBy(item => item.Brand);

            foreach (var group in groupedItems) {
                Echo.Print($"==========", ConsoleColor.Yellow);
                Echo.Print($">>>>>{group.Key}:", ConsoleColor.Yellow);
                Echo.Print($"==========", ConsoleColor.Yellow);
                group.ToList().ForEach(item => Console.WriteLine($"  {item.Model} - ${item.Price}"));
            }

            Console.WriteLine($"Enter the {category} brand you want to explore (or type 'back' to return to the main menu): ");
            string chosenBrand = Console.ReadLine();
            if (chosenBrand.ToLower() == "back") {
                menuHistory.Pop();
                DisplayMainMenu();
                return;
            }
            var selectedItems = items.Where(item => item.Brand.ToLower() == chosenBrand.ToLower()).ToList();

            if (selectedItems.Count == 0) {
                Console.WriteLine($"No {category.ToLower()} available for the chosen brand.");
                return;
            }

            Console.Clear();
            Echo.Print($"==========", ConsoleColor.Yellow);
            Echo.Print($">>>>>{chosenBrand}:", ConsoleColor.Yellow);
            Echo.Print($"==========", ConsoleColor.Yellow);
            Console.WriteLine($"Available models");

            selectedItems.ForEach(item => Console.WriteLine($"  {item.Model} - ${item.Price}"));

            Console.Write($"Enter the {category} model you want to purchase: ");
            string chosenModel = Console.ReadLine();
            //var selectedItem = selectedItems.FirstOrDefault(item => item.Model.ToLower() == chosenModel.ToLower());
            var selectedItem = selectedItems.FirstOrDefault(item => item.Model.Contains(chosenModel, StringComparison.OrdinalIgnoreCase));

            if (selectedItem != null) {
                Console.WriteLine($"You have chosen: {selectedItem}");
                Console.Write($"Do you want to add this {category.ToLower()} to your cart? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") {
                    cart.AddToCart(selectedItem);
                    Console.WriteLine($"{category} added to the cart.");
                }
            } else {
                Console.WriteLine($"Invalid {category.ToLower()} model selection.");
            }
        }

        private void Checkout() {
            Console.Clear();

            Echo.Print("-----------------------Cart & Checkout:-------------------------", ConsoleColor.Cyan);
            cart.ViewCart();

            Echo.Print("\nDo you want to proceed to checkout? (y/n): or Any Key to Back", ConsoleColor.Magenta);
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "y") {
                if (cart.IsEmpty()) {
                    Echo.Print("Empty Cart. Please add items to the cart before proceeding to checkout.", ConsoleColor.Yellow);
                } else {
                    Echo.Print("Order Placed \n", ConsoleColor.Green);
                    Echo.Print($"\n--------------------------------------------------------", ConsoleColor.Green);
                    //Console.WriteLine("\r\n\r\n___________.__                   __     _____.___.            ._. \r\n\\__    ___/|  |__ _____    ____ |  | __ \\__  |   | ____  __ __| | \r\n  |    |   |  |  \\\\__  \\  /    \\|  |/ /  /   |   |/  _ \\|  |  \\ | \r\n  |    |   |   Y  \\/ __ \\|   |  \\    <   \\____   (  <_> )  |  /\\| \r\n  |____|   |___|  (____  /___|  /__|_ \\  / ______|\\____/|____/ __ \r\n                \\/     \\/     \\/     \\/  \\/                    \\/ \r\n\r\n");
                    Echo.Print("\r\n\r\n___________.__                   __                                              \r\n\\__    ___/|  |__ _____    ____ |  | __  ______                                  \r\n  |    |   |  |  \\\\__  \\  /    \\|  |/ / /  ___/                                  \r\n  |    |   |   Y  \\/ __ \\|   |  \\    <  \\___ \\                                   \r\n  |____|   |___|  (____  /___|  /__|_ \\/____  >                                  \r\n                \\/     \\/     \\/     \\/     \\/                                   \r\n___________              _________.__                         .__                \r\n\\_   _____/__________   /   _____/|  |__   ____ ______ ______ |__| ____    ____  \r\n |    __)/  _ \\_  __ \\  \\_____  \\ |  |  \\ /  _ \\\\____ \\\\____ \\|  |/    \\  / ___\\ \r\n |     \\(  <_> )  | \\/  /        \\|   Y  (  <_> )  |_> >  |_> >  |   |  \\/ /_/  >\r\n \\___  / \\____/|__|    /_______  /|___|  /\\____/|   __/|   __/|__|___|  /\\___  / \r\n     \\/                        \\/      \\/       |__|   |__|           \\//_____/  \r\n\r\n", ConsoleColor.DarkGreen);
                    //Console.WriteLine("\r\n\r\n___________.__                   __                                              \r\n\\__    ___/|  |__ _____    ____ |  | __  ______                                  \r\n  |    |   |  |  \\\\__  \\  /    \\|  |/ / /  ___/                                  \r\n  |    |   |   Y  \\/ __ \\|   |  \\    <  \\___ \\                                   \r\n  |____|   |___|  (____  /___|  /__|_ \\/____  >                                  \r\n                \\/     \\/     \\/     \\/     \\/                                   \r\n___________              _________.__                         .__                \r\n\\_   _____/__________   /   _____/|  |__   ____ ______ ______ |__| ____    ____  \r\n |    __)/  _ \\_  __ \\  \\_____  \\ |  |  \\ /  _ \\\\____ \\\\____ \\|  |/    \\  / ___\\ \r\n |     \\(  <_> )  | \\/  /        \\|   Y  (  <_> )  |_> >  |_> >  |   |  \\/ /_/  >\r\n \\___  / \\____/|__|    /_______  /|___|  /\\____/|   __/|   __/|__|___|  /\\___  / \r\n     \\/                        \\/      \\/       |__|   |__|           \\//_____/  \r\n\r\n");
                    Echo.Print($"----------------------------------------------------------", ConsoleColor.Green);
                    cart.ClearCart();
                }
            }
            Console.WriteLine("Any key to back ");
            Console.ReadKey();
            menuHistory.Pop();
            DisplayMainMenu();
        }

        private void AddShopItems() {
            laptops.Add(new Laptop { Brand = "Apple", Model = "MacBook Pro 14", Price = 2499 });
            laptops.Add(new Laptop { Brand = "Apple", Model = "iPhone 14 Pro Max", Price = 1099 });
            laptops.Add(new Laptop { Brand = "Apple", Model = "iPad Air", Price = 599 });

            laptops.Add(new Laptop { Brand = "Dell", Model = "XPS 13", Price = 1299 });
            laptops.Add(new Laptop { Brand = "Dell", Model = "Inspiron 15", Price = 899 });
            laptops.Add(new Laptop { Brand = "Dell", Model = "Latitude 14", Price = 1499 });

            laptops.Add(new Laptop { Brand = "HP", Model = "Spectre x360", Price = 1399 });
            laptops.Add(new Laptop { Brand = "HP", Model = "Pavilion 15", Price = 749 });
            laptops.Add(new Laptop { Brand = "HP", Model = "Elite Dragonfly", Price = 1799 });

            laptops.Add(new Laptop { Brand = "Alienware", Model = "Area-51m", Price = 2999, GraphicsCard = "NVIDIA RTX 3080" });
            laptops.Add(new Laptop { Brand = "Asus", Model = "ROG Zephyrus G14", Price = 1799, GraphicsCard = "NVIDIA RTX 3060" });
            laptops.Add(new Laptop { Brand = "MSI", Model = "GS66 Stealth", Price = 2399, GraphicsCard = "NVIDIA RTX 3070" });


            // Adding accessories
            accessories.Add(new Accessory { Type = "Mouse", Brand = "Logitech", Model = "MX Master 3", Price = 99 });
            accessories.Add(new Accessory { Type = "Keyboard", Brand = "Corsair", Model = "K95 RGB Platinum", Price = 169 });
            accessories.Add(new Accessory { Type = "Headphones", Brand = "Sony", Model = "WH-1000XM4", Price = 299 });

            accessories.Add(new Accessory { Type = "Gaming Mouse", Brand = "Razer", Model = "DeathAdder Elite", Price = 69 });
            accessories.Add(new Accessory { Type = "Mechanical Keyboard", Brand = "SteelSeries", Model = "Apex Pro", Price = 199 });
            accessories.Add(new Accessory { Type = "Gaming Headset", Brand = "HyperX", Model = "Cloud II", Price = 99 });

        }
    }

}
