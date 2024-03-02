using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.Model
{
    class FilmSet
    {
        // The title to bind to the TextBlock (see FilmSetTheme.xaml in Themes)
        public string SetTitle { get; set; }
        // The list of film belongs to this set
        public ObservableCollection<Film> FilmList { get; set; }
    }
}
