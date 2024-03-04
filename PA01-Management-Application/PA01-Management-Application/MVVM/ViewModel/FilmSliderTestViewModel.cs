using PA01_Management_Application.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel
{
    /// <summary>
    /// Class to test the film slider, please navigate to FilmSliderTest.xaml to see usage
    /// </summary>
    class FilmSliderTestViewModel
    {
        // Danh sach cac slider film, moi element gom ten set va danh sach film
        // Sau nay dung EntityFramwork + LINQ de gan data 
        public ObservableCollection<FilmSet> FilmSets { get; set; }

        public FilmSliderTestViewModel()
        {
            FilmSets = new ObservableCollection<FilmSet>();

            for (int i = 0; i < 10; i++)
            {
                ObservableCollection<Film> filmList = new ObservableCollection<Film>();

                for (int j = 0; j < 10; j++)
                {
                    filmList.Add(new Film
                    (
                        $"Shrek {i}-{j}",
                       [$"Genre {i}-{j}"],
                        69 + i + j,
                        4.96,
                        "https://upload.wikimedia.org/wikipedia/vi/b/bb/Spy_×_Family_Code_White_Movie_Teaser_Visual.png",
                         "Videos/video.mp4",
                        []
                    ));
                }

                FilmSets.Add(new FilmSet()
                {
                    SetTitle = $"Set {i}",
                    FilmList = filmList
                });
            }
        }
    }
}
