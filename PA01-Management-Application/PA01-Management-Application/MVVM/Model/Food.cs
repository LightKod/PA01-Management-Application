using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.Model
{
    public class Food
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImagePath { get; set; }

        public Food()
        {
            ID = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Price = 0f;
            ImagePath = string.Empty;
        }

        public Food(string id, string name, string description, float price, string imagePath)
        {
            ID = id;
            Name = name;
            Description = description;
            Price = price;
            ImagePath = imagePath;
        }
    }
}
