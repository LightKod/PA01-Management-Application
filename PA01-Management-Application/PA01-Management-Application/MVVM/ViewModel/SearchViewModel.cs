using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class SearchViewModel : ObservableObject
    {
        MovieService service = new();

        private string _nameSearch;

        public string NameSearch
        {
            get { return _nameSearch; }
            set
            {
                _nameSearch = value;
                OnPropertyChanged();
            }
        }


        private string _actorSearch;

        public string ActorSearch
        {
            get { return _actorSearch; }
            set
            {
                _actorSearch = value;
                OnPropertyChanged();
            }
        }


        private string _directorSearch;

        public string DirectorSearch
        {
            get { return _directorSearch; }
            set
            {
                _directorSearch = value;
                OnPropertyChanged();
            }
        }

        private string _durationSearch;

        public string DurationSearch
        {
            get { return _durationSearch; }
            set
            {
                _durationSearch = value;
                OnPropertyChanged();
            }
        }

        private string _yearSearch;

        public string YearSearch
        {
            get { return _yearSearch; }
            set
            {
                _yearSearch = value;
                OnPropertyChanged();
            }
        }

        private string _searchResult;

        public string SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _maxPage;

        public int MaxPage
        {
            get { return _maxPage; }
            set
            {
                _maxPage = value;
                OnPropertyChanged();
            }
        }

        private int _maxItemsPerPage = 6;

        private ObservableCollection<Film> _filmList;

        public ObservableCollection<Film> FilmList
        {
            get { return _filmList; }
            set
            {
                _filmList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Film> DummyFilmList { get; set; }

        public ObservableCollection<Film> SearchResultList { get; set; }

        public RelayCommand SearchByNameCommand { get; set; }
        public RelayCommand PreviousPageCommand { get; set; }
        public RelayCommand NextPageCommand { get; set; }
        public RelayCommand AdvancedSearchCommand { get; set; }

        public ObservableCollection<String> ComboBoxOptions { get; set; }

        private string _selectedSort;

        public string SelectedSort
        {
            get { return _selectedSort; }
            set
            {
                _selectedSort = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SortCommand { get; set; }

        public MovieDetailViewModel MovieDetailVM { get; set; }
        public RelayCommand MovieDetailCommand {  get; set; }

        public SearchViewModel()
        {
            NameSearch = string.Empty;
            ActorSearch = string.Empty;
            DirectorSearch = string.Empty;
            DurationSearch = string.Empty;
            YearSearch = string.Empty;
            SearchResult = string.Empty;
            CurrentPage = 0;
            MaxPage = 0;

            ComboBoxOptions = new ObservableCollection<String>() { "Name, A-Z", "Name, Z-A",
                                                                   "Duration, Ascending", "Duration, Descending",
                                                                   "Rating, Ascending", "Rating, Descending" };

            SearchResultList = new ObservableCollection<Film>();

            SearchByNameCommand = new RelayCommand(o =>
            {
                SearchByName();
            });

            PreviousPageCommand = new RelayCommand(o =>
            {
                CurrentPage--;
                FilmList = new ObservableCollection<Film>(SearchResultList.Skip((CurrentPage - 1) * _maxItemsPerPage).Take(_maxItemsPerPage).ToList());
            });

            NextPageCommand = new RelayCommand(o =>
            {
                CurrentPage++;
                FilmList = new ObservableCollection<Film>(SearchResultList.Skip((CurrentPage - 1) * _maxItemsPerPage).Take(_maxItemsPerPage).ToList());
            });

            AdvancedSearchCommand = new RelayCommand(o =>
            {
                SearchWithExtraInfo();
            });

            SortCommand = new RelayCommand(o =>
            {
                if (SelectedSort != null)
                {
                    if (SelectedSort == "Name, A-Z")
                    {
                        SearchResultList = new ObservableCollection<Film>(SearchResultList.OrderBy(film => film.FilmName).ToList());
                    }
                    else if (SelectedSort == "Name, Z-A")
                    {
                        SearchResultList = new ObservableCollection<Film>(SearchResultList.OrderByDescending(film => film.FilmName).ToList());
                    }
                    else if (SelectedSort == "Duration, Ascending")
                    {
                        SearchResultList = new ObservableCollection<Film>(SearchResultList.OrderBy(film => film.FilmDuration).ToList());
                    }
                    else if (SelectedSort == "Duration, Descending")
                    {
                        SearchResultList = new ObservableCollection<Film>(SearchResultList.OrderByDescending(film => film.FilmDuration).ToList());
                    }
                    else if (SelectedSort == "Rating, Ascending")
                    {
                        SearchResultList = new ObservableCollection<Film>(SearchResultList.OrderBy(film => film.FilmRating).ToList());
                    }
                    else if (SelectedSort == "Rating, Descending")
                    {
                        SearchResultList = new ObservableCollection<Film>(SearchResultList.OrderByDescending(film => film.FilmRating).ToList());
                    }

                    CurrentPage = 1;
                    FilmList = new ObservableCollection<Film>(SearchResultList.Take(_maxItemsPerPage).ToList());
                }
            });

            MovieDetailVM = new MovieDetailViewModel();
            MovieDetailCommand = new RelayCommand(o =>
            {
                if (o is Film selectedFilm)
                {
                    BookingDataHolder.movieID = selectedFilm.FilmID;
                    (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = MovieDetailVM;
                }
            });
        }

        public async void SearchByName()
        {
            if (NameSearch != string.Empty)
            {
                /*SearchResultList = new ObservableCollection<Film>(DummyFilmList.Where(film => film.FilmName.Contains(NameSearch, StringComparison.OrdinalIgnoreCase)).OrderBy(film => film.FilmName).ToList());*/
                await GetAllMoviesByName();
                SelectedSort = "Name, A-Z";
                if (SearchResultList.Count > 0)
                {
                    SearchResult = $"Found {SearchResultList.Count} film{(SearchResultList.Count > 1 ? "s" : "")}";
                    MaxPage = (int)Math.Ceiling((double)SearchResultList.Count / _maxItemsPerPage);
                    CurrentPage = 1;
                    FilmList = new ObservableCollection<Film>(SearchResultList.Take(_maxItemsPerPage).ToList());
                }
                else
                {
                    SearchResult = "There are no films matched the search";
                    FilmList = new ObservableCollection<Film>();
                    MaxPage = 0;
                    CurrentPage = 0;
                }
            }
        }

        public async Task GetAllMoviesByName()
        {
            var searchResult = service.SearchMoviesByTitle(NameSearch);
            SearchResultList.Clear();
            foreach (var movie in searchResult)
            {
                Film f = new();

                f.FilmID = movie.MovieId;
                f.FilmName = movie.Title;
                f.FilmGenres = await service.GetGenresByMovieIdAsync(movie.MovieId);
                f.FilmDuration = movie.RunTime.Value;
                f.FilmRating = movie.VoteAverage.Value;
                f.FilmTrailer = "Videos/video.mp4";
                f.FilmBanner = new string[] { movie.BackdropPath };
                f.FilmPoster = movie.PosterPath;
                f.Directors = new string[] { await service.GetDirectorNameByMovieIdAsync(movie.MovieId) };
                f.Stars = await service.GetActorsByMovieIdAsync(movie.MovieId);
                f.FilmDescription = movie.Overview;

                SearchResultList.Add(f);
            };
        }

        public async void SearchWithExtraInfo()
        {
            await GetAllMoviesWithExtraInfo();
            SelectedSort = "Name, A-Z";
            if (SearchResultList.Count > 0)
            {
                SearchResult = $"Found {SearchResultList.Count} film{(SearchResultList.Count > 1 ? "s" : "")}";
                MaxPage = (int)Math.Ceiling((double)SearchResultList.Count / _maxItemsPerPage);
                CurrentPage = 1;
                FilmList = new ObservableCollection<Film>(SearchResultList.Take(_maxItemsPerPage).ToList());
            }
            else
            {
                SearchResult = "There are no films matched the search";
                FilmList = new ObservableCollection<Film>();
                MaxPage = 0;
                CurrentPage = 0;
            }
        }

        public async Task GetAllMoviesWithExtraInfo()
        {
            var searchResult = service.SearchMovies(NameSearch, DirectorSearch, ActorSearch, YearSearch == string.Empty ? 0 : Int32.Parse(YearSearch));
            SearchResultList.Clear();
            foreach (var movie in searchResult)
            {
                Film f = new();

                f.FilmID = movie.MovieId;
                f.FilmName = movie.Title;
                f.FilmGenres = await service.GetGenresByMovieIdAsync(movie.MovieId);
                f.FilmDuration = movie.RunTime.Value;
                f.FilmRating = movie.VoteAverage.Value;
                f.FilmTrailer = "Videos/video.mp4";
                f.FilmBanner = new string[] { movie.BackdropPath };
                f.FilmPoster = movie.PosterPath;
                f.Directors = new string[] { await service.GetDirectorNameByMovieIdAsync(movie.MovieId) };
                f.Stars = await service.GetActorsByMovieIdAsync(movie.MovieId);
                f.FilmDescription = movie.Overview;

                SearchResultList.Add(f);
            };
        }
    }
}
