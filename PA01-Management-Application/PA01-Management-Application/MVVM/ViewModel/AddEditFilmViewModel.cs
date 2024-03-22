using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class AddEditFilmViewModel : ObservableObject
    {
        MovieService service = new();

        private bool _isEditing;

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                OnPropertyChanged();
            }
        }

        private bool _isValidForm;

        public bool IsValidForm
        {
            get { return _isValidForm; }
            set
            {
                _isValidForm = value;
                OnPropertyChanged();
            }
        }

        private int _filmId;

        public int FilmId
        {
            get { return _filmId; }
            set { _filmId = value; }
        }


        private string _filmName;

        public string FilmName
        {
            get { return _filmName; }
            set
            {
                _filmName = value;
                OnPropertyChanged();
            }
        }

        private int _filmDuration;

        public int FilmDuration
        {
            get { return _filmDuration; }
            set
            {
                _filmDuration = value;
                OnPropertyChanged();
            }
        }

        private double _filmRating;

        public double FilmRating
        {
            get { return _filmRating; }
            set
            {
                _filmRating = value;
                OnPropertyChanged();
            }
        }

        private string _filmPoster;

        public string FilmPoster
        {
            get { return _filmPoster; }
            set
            {
                _filmPoster = value;
                OnPropertyChanged();
            }
        }

        private string _filmBanner;

        public string FilmBanner
        {
            get { return _filmBanner; }
            set
            {
                _filmBanner = value;
                OnPropertyChanged();
            }
        }

        private string _filmDescription;

        public string FilmDescription
        {
            get { return _filmDescription; }
            set
            {
                _filmDescription = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _filmGenres;

        public ObservableCollection<string> FilmGenres
        {
            get { return _filmGenres; }
            set
            {
                _filmGenres = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Tuple<string, string>> _filmPeople;

        public ObservableCollection<Tuple<string, string>> FilmPeople
        {
            get { return _filmPeople; }
            set
            {
                _filmPeople = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _genreComboBoxOptions;

        public ObservableCollection<string> GenreComboBoxOptions
        {
            get { return _genreComboBoxOptions; }
            set
            {
                _genreComboBoxOptions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _peopleComboBoxOptions;

        public ObservableCollection<string> PeopleComboBoxOptions
        {
            get { return _peopleComboBoxOptions; }
            set
            {
                _peopleComboBoxOptions = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> RoleComboBoxOptions { get; set; }

        public RelayCommand GetGenreCommand { get; set; }
        public RelayCommand GetPeopleCommand { get; set; }
        public RelayCommand SaveGenreCommand { get; set; }
        public RelayCommand SavePersonCommand { get; set; }
        public RelayCommand DeleteGenreCommand { get; set; }
        public RelayCommand DeletePersonCommand { get; set; }
        public RelayCommand CheckFormValidityCommand { get; set; }

        public AddEditFilmViewModel(bool isEdit = false, Film? filmToEdit = null)
        {
            IsValidForm = false;

            if (isEdit && filmToEdit != null)
            {
                IsEditing = true;
                FilmId = filmToEdit.FilmID;
                FilmName = filmToEdit.FilmName;
                FilmDuration = filmToEdit.FilmDuration;
                FilmRating = filmToEdit.FilmRating;
                FilmPoster = filmToEdit.FilmPoster;
                FilmBanner = filmToEdit.FilmBanner[0];
                FilmDescription = filmToEdit.FilmDescription;
                FilmGenres = new ObservableCollection<string>(filmToEdit.FilmGenres);
                FilmPeople = new ObservableCollection<Tuple<string, string>>();
                foreach (var item in filmToEdit.Directors)
                {
                    FilmPeople.Add(new Tuple<string, string>(item, "Director"));
                }
                foreach (var item in filmToEdit.Stars)
                {
                    FilmPeople.Add(new Tuple<string, string>(item, "Actor"));
                }
            }
            else
            {
                IsEditing = false;
                FilmId = 0;
                FilmName = String.Empty;
                FilmDuration = 0;
                FilmRating = 0f;
                FilmPoster = String.Empty;
                FilmBanner = String.Empty;
                FilmDescription = String.Empty;
                FilmGenres = new ObservableCollection<string>();
                FilmPeople = new ObservableCollection<Tuple<string, string>>();
            }

            RoleComboBoxOptions = new ObservableCollection<string>
            {
                "Director",
                "Actor"
            };

            // Refresh the ComboBoxes' options whenever an add request is called
            GetGenreCommand = new RelayCommand(o =>
            {
               GetGenreNames();
            });

            GetPeopleCommand = new RelayCommand(o =>
            {
                GetPeopleNames();
            });

            SaveGenreCommand = new RelayCommand(o =>
            {
                if (o is string genreName)
                {
                    if (!FilmGenres.Contains(genreName))
                    {
                        FilmGenres.Add(genreName);
                    }
                }
            });

            SavePersonCommand = new RelayCommand(o =>
            {
                if (o is Tuple<string, string> person)
                {
                    if (!FilmPeople.Contains(person))
                    {
                        FilmPeople.Add(person);
                    }
                }
            });

            DeleteGenreCommand = new RelayCommand(o =>
            {
                if (o is string genreName)
                {
                    if (FilmGenres.Contains(genreName))
                    {
                        FilmGenres.Remove(genreName);
                    }
                }
            });

            DeletePersonCommand = new RelayCommand(o =>
            {
                if (o is Tuple<string, string> person)
                {
                    if (FilmPeople.Contains(person))
                    {
                        FilmPeople.Remove(person);
                    }
                }
            });

            CheckFormValidityCommand = new RelayCommand(o =>
            {
                if (FilmName == string.Empty || FilmDuration <= 0 || FilmRating <= 0 || FilmPoster == string.Empty || FilmBanner == string.Empty || FilmDescription == string.Empty || FilmGenres.Count == 0 || FilmPeople.Count == 0)
                {
                    IsValidForm = false;
                }
                else
                {
                    IsValidForm = true;
                }
            });
        }

        private async void GetGenreNames()
        {
            GenreComboBoxOptions = new ObservableCollection<string>(service.GetAllGenreNames());
        }

        private async void GetPeopleNames()
        {
            PeopleComboBoxOptions = new ObservableCollection<string>(service.GetAllPersonNames());
        }

        public Film BuildFilm()
        {
            Film film = new();

            film.FilmID = FilmId;
            film.FilmName = FilmName;
            film.FilmGenres = FilmGenres.ToArray();
            film.FilmDuration = FilmDuration;
            film.FilmRating = FilmRating;
            film.FilmTrailer = "Videos/video.mp4";
            film.FilmBanner = new string[] { FilmBanner };
            film.FilmPoster = FilmPoster;
            film.Directors = FilmPeople.Where(p => p.Item2 == "Director").Select(p => p.Item1).ToArray();
            film.Stars = FilmPeople.Where(p => p.Item2 == "Actor").Select(p => p.Item1).ToArray();
            film.FilmDescription = FilmDescription;

            return film;
        }
    }
}
