using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleShop {
    abstract class ShopItem {
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }

        public override string ToString() => $"{Brand} {Model} - ${Price}";
    }

}
