using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PA01_Management_Application.MVVM.Models;
using PA01_Management_Application.MVVM.ViewModel;
using PA01_Management_Application.MVVM.ViewModel.Service;

namespace PA01_Management_Application.MVVM.View
{
    public partial class ReportPageView : UserControl
    {
        private ReportViewModel _viewModel;

        MovieService movieService = new MovieService();
        ReportService reportService = new ReportService();

        Movie movie = new Movie();

        List<(DateTime? date, double? price)> reportS = new List<(DateTime? date, double? price)>();

        public ReportPageView()
        {
            InitializeComponent();

            _viewModel = new ReportViewModel();
            DataContext = _viewModel;

            _viewModel.Movies = movieService.GetAllMovies();

            MovieFilterComboBox.Items.Insert(0, "All Movies");

            foreach (var movie in _viewModel.Movies)
            {
                MovieFilterComboBox.Items.Add(movie);
            }

            DateFilterComboBox.SelectionChanged += ApplyFilters;

            MovieFilterComboBox.SelectionChanged += ApplyFilters;

            StartDatePicker.SelectedDateChanged += StartDatePicker_SelectedDateChanged;
            EndDatePicker.SelectedDateChanged += EndDatePicker_SelectedDateChanged;

            UpdateChart();
        }

        private void ApplyFilters(object sender, SelectionChangedEventArgs e)
        {

            UpdateChart();
        }

        private async void UpdateChart()
        {

            // Get selected filter values
            var selectedDateFilter = (DateFilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (MovieFilterComboBox.SelectedItem is PA01_Management_Application.MVVM.Models.Movie)
            {
                movie = (Movie)MovieFilterComboBox.SelectedItem;
            }

            if (movie != null)
            {
                switch (selectedDateFilter)
                {
                    case "Day to Day":
                        UpdateChartByDay();
                        break;
                    case "By Week":
                        UpdateChartByWeek();
                        break;
                    case "By Month":
                        UpdateChartByMonth();
                        break;
                    case "By Year":
                        UpdateChartByYear();
                        break;
                }
            }

        }

        private void DateFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as ComboBox).SelectedItem as ComboBoxItem;

            // Nếu lựa chọn là "Day to Day", hiển thị hai DatePicker để chọn ngày bắt đầu và kết thúc
            if (selectedItem != null && selectedItem.Content.ToString() == "Day to Day")
            {
                StartDatePicker.Visibility = System.Windows.Visibility.Visible;
                EndDatePicker.Visibility = System.Windows.Visibility.Visible;

                UpdateChart();
            }
            else
            {
                StartDatePicker.Visibility = System.Windows.Visibility.Collapsed;
                EndDatePicker.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private async void UpdateChartByDay()
        {
            var startDate = StartDatePicker.SelectedDate;
            var endDate = EndDatePicker.SelectedDate;

            if (startDate == null || endDate == null)
            {
                return;
            }
            var dateRange = new List<DateTime>();
            for (var date = startDate.Value.Date; date <= endDate.Value.Date; date = date.AddDays(1))
            {
                dateRange.Add(date);
            }

            var bookingCounts = new Dictionary<DateTime, double>();
            reportS = await reportService.GetBookingsByMovieIdAndDateRangeAsync(movie.MovieId, startDate.Value, endDate.Value);
            if (reportS != null)
            {
                foreach (var date in dateRange)
                {
                    int check = 0;
                    foreach (var item in reportS)
                    {
                        if (item.date == date.Date)
                        {
                            check = 1;
                            bookingCounts.Add(date, item.price.Value);
                        }

                    }
                    if (check == 0)
                    {
                        bookingCounts.Add(date, 0);
                    }
                }
            }

            var dayChartValues = new ChartValues<ObservablePoint>();
            foreach (var kvp in bookingCounts)
            {
                dayChartValues.Add(new ObservablePoint(dayChartValues.Count + 1, kvp.Value));


                //Cập nhật dữ liệu và nhãn trục x của biểu đồ
                (ReportChart.Series[0] as LineSeries).Values = dayChartValues;
                ReportChart.AxisX[0].Labels = dateRange.Select(date => date.ToString("dd/MM/yyyy")).ToList();
            }
        }

        private async void UpdateChartByWeek()
        {
            // Ngày bắt đầu mặc định
            var defaultStartDate = new DateTime(2024, 3, 13);

            var dateRange = new List<DateTime>();
            for (var date = defaultStartDate; date <= DateTime.Today; date = date.AddDays(1))
            {
                dateRange.Add(date);
            }

            var bookingCounts = new Dictionary<DateTime, double>();
            reportS = await reportService.GetBookingsByMovieIdAndDateRangeAsync(movie.MovieId, defaultStartDate, DateTime.Today);
            if (reportS != null)
            {
                foreach (var date in dateRange)
                {
                    int check = 0;
                    foreach (var item in reportS)
                    {
                        if (item.date == date.Date)
                        {
                            check = 1;
                            bookingCounts.Add(date, item.price.Value);
                        }

                    }
                    if (check == 0)
                    {
                        bookingCounts.Add(date, 0);
                    }
                }
            }

            var dayChartValues = new ChartValues<ObservablePoint>();
            foreach (var kvp in bookingCounts)
            {
                dayChartValues.Add(new ObservablePoint(dayChartValues.Count + 1, kvp.Value));


                //Cập nhật dữ liệu và nhãn trục x của biểu đồ
                (ReportChart.Series[0] as LineSeries).Values = dayChartValues;
                ReportChart.AxisX[0].Labels = dateRange.Select(date => date.ToString("dd/MM/yyyy")).ToList();
            }
        }

        private async void UpdateChartByMonth()
        {
            var projectStartDate = new DateTime(2024, 1, 1);

            var dateRange = new List<DateTime>();
            for (var date = projectStartDate; date <= DateTime.Today; date = date.AddMonths(1))
            {
                dateRange.Add(date);
            }

            var bookingCountsByMonth = new Dictionary<DateTime, double>();

            foreach (var month in dateRange)
            {
                var bookings = await reportService.GetBookingsByMovieIdAndDateRangeAsync(movie.MovieId, month, month.AddMonths(1).AddDays(-1));
                var totalBookingPrice = bookings.Sum(b => b.price ?? 0);
                bookingCountsByMonth.Add(month, totalBookingPrice);
            }

            var monthChartValues = new ChartValues<ObservablePoint>();

            foreach (var month in dateRange)
            {
                monthChartValues.Add(new ObservablePoint(monthChartValues.Count + 1, bookingCountsByMonth.ContainsKey(month) ? bookingCountsByMonth[month] : 0));
            }

    (ReportChart.Series[0] as LineSeries).Values = monthChartValues;
            ReportChart.AxisX[0].Labels = dateRange.Select(date => date.ToString("MM/yyyy")).ToList();
        }

        private async void UpdateChartByYear()
        {
            var projectStartDate = new DateTime(2010, 1, 1);

            var dateRange = new List<DateTime>();
            for (var date = projectStartDate; date <= DateTime.Today; date = date.AddYears(1))
            {
                dateRange.Add(date);
            }

            var bookingCountsByYear = new Dictionary<int, double>();

            foreach (var year in dateRange)
            {
                var startDate = new DateTime(year.Year, 1, 1);
                var endDate = startDate.AddYears(1).AddDays(-1);

                var bookings = await reportService.GetBookingsByMovieIdAndDateRangeAsync(movie.MovieId, startDate, endDate);
                var totalBookingPrice = bookings.Sum(b => b.price ?? 0);
                bookingCountsByYear.Add(year.Year, totalBookingPrice);
            }

            var yearChartValues = new ChartValues<ObservablePoint>();

            foreach (var year in dateRange)
            {
                yearChartValues.Add(new ObservablePoint(yearChartValues.Count + 1, bookingCountsByYear.ContainsKey(year.Year) ? bookingCountsByYear[year.Year] : 0));
            }

    (ReportChart.Series[0] as LineSeries).Values = yearChartValues;
            ReportChart.AxisX[0].Labels = dateRange.Select(date => date.Year.ToString()).ToList();
        }

    }

}
