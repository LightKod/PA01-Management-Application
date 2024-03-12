using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.Model
{
    class Room
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<Seat> Seats { get; set; }


        public int Rows {  get; set; }
        public int Columns {  get; set; }

        public Room(string id, string name, List<Seat> seats, int rows, int columns)
        {
            ID = id;
            Name = name;
            Seats = seats;
            Rows = rows;
            Columns = columns;
        }

        public Room(Room other)
        {
            ID = other.ID;
            Name = other.Name;
            Rows = other.Rows;
            Columns = other.Columns;

            Seats = new List<Seat>();
            foreach (Seat seat in other.Seats)
            {
                Seats.Add(new Seat(seat.Name, seat.Type));
            }
        }
    }
}
