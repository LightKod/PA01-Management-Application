using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.Model
{
    public class Item
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public Item(string name, float price)
        {
            Name = name;
            Price = price;
        }
    }
}
