using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.Model
{
    class Person
    {
        public string ID;
        public string Name;
        public string Avatar;
        public string Biography;

        public Person()
        {
            ID = string.Empty;
            Name = string.Empty;
            Avatar = string.Empty;
            Biography = string.Empty;
        }

        public Person(string id, string name, string avatar, string biography)
        {
            ID = id;
            Name = name;
            Avatar = avatar;
            Biography = biography;
        }
    }
}
