using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.DataManagers
{
    static class BookingDataHolder
    {
        public static int movieID  = 9502;
        public static Film film;
        public static Movie movie;
        public static Schedule schedule;
        public static PA01_Management_Application.MVVM.Model.Room room;
        public static List<Seat> seats;
        public static ObservableCollection<KeyValuePair<PA01_Management_Application.MVVM.Models.Food, int>> foods;
    }
}
