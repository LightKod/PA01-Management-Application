using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Model;
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
                FilmName = filmToEdit.FilmName;
                FilmDuration = filmToEdit.FilmDuration;
                FilmRating = filmToEdit.FilmRating;
                FilmPoster = filmToEdit.FilmPoster;
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
                FilmName = String.Empty;
                FilmDuration = 0;
                FilmRating = 0f;
                FilmPoster = String.Empty;
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
                GenreComboBoxOptions = GetGenreNames();
            });

            GetPeopleCommand = new RelayCommand(o =>
            {
                PeopleComboBoxOptions = GetPeopleNames();
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
                if (FilmName == string.Empty || FilmDuration <= 0 || FilmRating <= 0 || FilmPoster == string.Empty || FilmGenres.Count == 0 || FilmPeople.Count == 0)
                {
                    IsValidForm = false;
                }
                else
                {
                    IsValidForm = true;
                }
            });
        }

        private ObservableCollection<string> GetGenreNames()
        {
            return new ObservableCollection<string>
            {
                "Horror",
                "Action",
                "Romance",
                "Sci-fi"
            };
        }

        private ObservableCollection<string> GetPeopleNames()
        {
            return new ObservableCollection<string>
            {
                "Pickle",
                "Adam",
                "Eve",
                "ILoveMyHand",
                "WhoAreYou"
            };
        }

        public Film BuildFilm()
        {
            Film film = new Film
                (
                    FilmName,
                    FilmGenres.ToArray(),
                    FilmDuration,
                    FilmRating,
                    FilmPoster,
                    "Videos/video.mp4",
                    [],
                    FilmPeople.Where(p => p.Item2 == "Director").Select(p => p.Item1).ToArray(),
                    [],
                    FilmPeople.Where(p => p.Item2 == "Actor").Select(p => p.Item1).ToArray()
                );
            return film;
        }
    }
}
