﻿using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class SearchViewModel : ObservableObject
    {
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

        private string _timeSearch;

        public string TimeSearch
        {
            get { return _timeSearch; }
            set
            {
                _timeSearch = value;
                OnPropertyChanged();
            }
        }

        private string _priceSearch;

        public string PriceSearch
        {
            get { return _priceSearch; }
            set
            {
                _priceSearch = value;
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

        public SearchViewModel()
        {
            NameSearch = string.Empty;
            ActorSearch = string.Empty;
            DirectorSearch = string.Empty;
            TimeSearch = string.Empty;
            PriceSearch = string.Empty;
            YearSearch = string.Empty;
            SearchResult = string.Empty;
            CurrentPage = 0;
            MaxPage = 0;

            ComboBoxOptions = new ObservableCollection<String>() { "Name, A-Z", "Name, Z-A",
                                                                   "Duration, Ascending", "Duration, Descending",
                                                                   "Rating, Ascending", "Rating, Descending" };

            DummyFilmList = new ObservableCollection<Film>();
            DummyFilmList.Add(new Film
                    (
                        $"This is a film with a very long title to test the capability of the ItemsControl",
                       [$"Genre"],
                        69,
                        4.96,
                        "https://upload.wikimedia.org/wikipedia/vi/b/bb/Spy_×_Family_Code_White_Movie_Teaser_Visual.png",
                         "Videos/video.mp4",
                        [], ["Meo meo bruh", "Huhu"], [], []
                    ));
            for (int j = 0; j < 9; j++)
            {
                DummyFilmList.Add(new Film
                    (
                        $"Shrek {j}",
                       [$"Genre {j}"],
                        69 - j,
                        4.96 + j*0.1,
                        j % 3 == 0 ? "https://upload.wikimedia.org/wikipedia/vi/b/bb/Spy_×_Family_Code_White_Movie_Teaser_Visual.png" : "https://www.elle.vn/wp-content/uploads/2023/12/06/560540/poster-Mai-scaled.jpg",
                         "Videos/video.mp4",
                        [],
                        j % 2 == 0 ? ["Pickle", $"{(j % 3 == 0 ? "Bruh" : "Lmao")}"] : ["Doggy", $"{(j % 3 == 0 ? "Lmao" : "Bruh")}"],
                        [], []
                    ));
            }
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
                SearchResultList = new ObservableCollection<Film>(DummyFilmList.Where(film => film.FilmName.Contains(NameSearch, StringComparison.OrdinalIgnoreCase) &&
                                                                                              (film.Directors != null && film.Directors.Any(director => director.Contains(DirectorSearch, StringComparison.OrdinalIgnoreCase))))
                                                                               .OrderBy(film => film.FilmName)
                                                                               .ToList());
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
        }

        public void SearchByName()
        {
            if (NameSearch != string.Empty)
            {
                SearchResultList = new ObservableCollection<Film>(DummyFilmList.Where(film => film.FilmName.Contains(NameSearch, StringComparison.OrdinalIgnoreCase)).OrderBy(film => film.FilmName).ToList());
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
    }
}
