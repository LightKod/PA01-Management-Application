using PA01_Management_Application.MVVM.Models;
using PA01_Management_Application.MVVM.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PA01_Management_Application.MVVM.ViewModel
{
    public class PurchasedTicketViewModel : INotifyPropertyChanged
    {
        private readonly IPurchasedTicketService _purchasedTicketService;
        private ObservableCollection<PurchasedTicket> _purchasedTickets;
        private int _currentPage = 1;
        private const int PageSize = 10;

        public ObservableCollection<PurchasedTicket> PurchasedTickets
        {
            get => _purchasedTickets;
            set
            {
                _purchasedTickets = value;
                OnPropertyChanged();
            }
        }

        private int _selectedBookingCode;

        public int SelectedBookingCode
        {
            get { return _selectedBookingCode; }
            set
            {
                _selectedBookingCode = value;
                OnPropertyChanged();
            }
        }

        public PurchasedTicketViewModel(IPurchasedTicketService purchasedTicketService, int userId)
        {
            _purchasedTicketService = purchasedTicketService;
            LoadPurchasedTicketsForUser(_currentPage, userId);
        }

        public async Task LoadPurchasedTicketsForUser(int page, int userId)
        {
            try
            {
                //var userId = 1; 
                var startIndex = (page - 1) * PageSize;
                var purchasedTickets = await Task.Run(() => _purchasedTicketService.GetPurchasedTicketsForUser(userId, page, PageSize));
                PurchasedTickets = new ObservableCollection<PurchasedTicket>(MapToViewModel(purchasedTickets));
                Console.WriteLine($"Tổng số vé mua: {PurchasedTickets.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        public async Task NextPage(int userId)
        {
            var totalTickets = await _purchasedTicketService.GetTotalPurchasedTicketsCount(1);
            if (_currentPage * PageSize < totalTickets)
            {
                _currentPage++;
                await LoadPurchasedTicketsForUser(_currentPage, userId);
            }
        }


        public async Task PreviousPage(int userId)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadPurchasedTicketsForUser(_currentPage, userId);
            }
        }

        private List<PurchasedTicket> MapToViewModel(List<PurchasedTicket> purchasedTickets)
        {
            var viewModelList = purchasedTickets
                .OrderByDescending(ticket => ticket.ReleaseDate)
                .ThenBy(ticket => ticket.Time)
                .GroupBy(ticket => new
                {
                    ticket.FilmName,
                    ticket.Time,
                    ticket.Cinema,
                    ticket.CinemaLocation
                })
                .Select(group =>
                {
                    var totalPrice = group.Sum(ticket => ticket.Price ?? 0);
                    var seats = string.Join(", ", group.Select(ticket => ticket.Seat));
                    var firstTicket = group.First();

                    return new PurchasedTicket
                    {
                        BookingCode = firstTicket.BookingCode,
                        FilmPoster = firstTicket.FilmPoster,
                        FilmName = firstTicket.FilmName,
                        FilmGenres = firstTicket.FilmGenres,
                        FilmDuration = firstTicket.FilmDuration,
                        FilmRating = firstTicket.FilmRating,
                        ReleaseDate = firstTicket.ReleaseDate,
                        Time = firstTicket.Time,
                        Cinema = firstTicket.Cinema,
                        CinemaLocation = firstTicket.CinemaLocation,
                        Seat = seats,
                        Price = totalPrice
                    };
                })
                .ToList();

            return viewModelList;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

