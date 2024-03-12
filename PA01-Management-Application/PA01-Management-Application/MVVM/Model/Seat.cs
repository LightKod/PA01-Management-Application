using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.Model
{
    public enum SeatType : int
    {
        None = -1,
        Normal = 0,
        Picked = 1,
        Booked = 2,
        VIP = 3,
    }

    class Seat
    {
        public string Name { get; set; }
        public SeatType Type { get; set; }
        public Seat(string name, SeatType type)
        {
            Name = name;
            Type = type;
        }

        public string SeatTypeString
        {
            get {
                return Type.ToString();
            }
            set
            {
                 
            }
        }
    }
}
