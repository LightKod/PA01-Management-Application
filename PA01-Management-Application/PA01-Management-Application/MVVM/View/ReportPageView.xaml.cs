using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.ViewModel;

namespace PA01_Management_Application.MVVM.View
{
    public partial class ReportPageView : UserControl
    {
        private BookingViewModel _viewModel;
        private string selectedMovie; 


        public ReportPageView()
        {
            InitializeComponent();

            _viewModel = new BookingViewModel();
            DataContext = _viewModel;

            // Populate movie filter ComboBox
            var movies = _viewModel.Bookings.Select(b => b.Movie).Distinct().ToList();
            MovieFilterComboBox.Items.Insert(0, "All Movies");

            foreach (var movie in movies)
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

        private void UpdateChart()
        {
            // Get selected filter values
            var selectedDateFilter = (DateFilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            selectedMovie = MovieFilterComboBox.SelectedItem?.ToString(); // Sử dụng biến selectedMovie ở đây

            var filteredBookings = _viewModel.Bookings.ToList();

            // Apply date filter
            switch (selectedDateFilter)
            {
                case "Day to Day":
                    UpdateChartByDay(filteredBookings);
                    break;
                case "By Week":
                    UpdateChartByWeek(filteredBookings);
                    break;
                case "By Month":
                    UpdateChartByMonth(filteredBookings);
                    break;
                case "By Year":
                    UpdateChartByYear(filteredBookings);
                    break;
            }

            if (selectedMovie != "All Movies" && !string.IsNullOrEmpty(selectedMovie))
            {
                filteredBookings = filteredBookings.Where(b => b.Movie == selectedMovie).ToList();
            }

            var chartValues = new ChartValues<ObservablePoint>();
            foreach (var booking in filteredBookings)
            {
                chartValues.Add(new ObservablePoint(chartValues.Count + 1, booking.Price));
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


        private void UpdateChartByMonth(List<Booking> filteredBookings)
        {
            var monthYearList = new List<string>();

            // Tạo danh sách các tháng và năm có trong dữ liệu
            foreach (var booking in filteredBookings)
            {
                var monthYear = $"{booking.BookingDate.ToString("MMMM")}, {booking.BookingDate.Year}";
                if (!monthYearList.Contains(monthYear))
                {
                    monthYearList.Add(monthYear);
                }
            }

            // Sắp xếp danh sách theo thứ tự thời gian
            monthYearList = monthYearList.OrderBy(x => DateTime.ParseExact(x, "MMMM, yyyy", CultureInfo.InvariantCulture)).ToList();

            var monthChartValues = new ChartValues<ObservablePoint>();

            // Đếm số lượng đặt vé cho mỗi tháng
            foreach (var monthYear in monthYearList)
            {
                var filteredByMonth = filteredBookings.Where(b =>
                    $"{b.BookingDate.ToString("MMMM")}, {b.BookingDate.Year}" == monthYear).ToList();

                // Áp dụng bộ lọc chọn phim
                if (!string.IsNullOrEmpty(selectedMovie) && selectedMovie != "All Movies")
                {
                    filteredByMonth = filteredByMonth.Where(b => b.Movie == selectedMovie).ToList();
                }

                var count = filteredByMonth.Count;

                monthChartValues.Add(new ObservablePoint(monthChartValues.Count + 1, count));
            }

            // Cập nhật dữ liệu và nhãn trục x của biểu đồ
            (ReportChart.Series[0] as LineSeries).Values = monthChartValues;
            ReportChart.AxisX[0].Labels = monthYearList;
        }

        private void UpdateChartByYear(List<Booking> filteredBookings)
        {
            var yearList = new List<string>();

            // Tạo danh sách các năm có trong dữ liệu
            foreach (var booking in filteredBookings)
            {
                var year = $"{booking.BookingDate.Year}";
                if (!yearList.Contains(year))
                {
                    yearList.Add(year);
                }
            }

            // Sắp xếp danh sách theo thứ tự thời gian
            yearList = yearList.OrderBy(x => DateTime.ParseExact(x, "yyyy", CultureInfo.InvariantCulture)).ToList();

            var yearChartValues = new ChartValues<ObservablePoint>();

            // Đếm số lượng đặt vé cho mỗi năm
            foreach (var yearr in yearList)
            {
                var filteredByYear = filteredBookings.Where(b =>
                    $"{b.BookingDate.Year}" == yearr).ToList();

                // Áp dụng bộ lọc chọn phim
                if (!string.IsNullOrEmpty(selectedMovie) && selectedMovie != "All Movies")
                {
                    filteredByYear = filteredByYear.Where(b => b.Movie == selectedMovie).ToList();
                }

                var count = filteredByYear.Count;

                yearChartValues.Add(new ObservablePoint(yearChartValues.Count + 1, count));
            }

    // Cập nhật dữ liệu và nhãn trục x của biểu đồ
    (ReportChart.Series[0] as LineSeries).Values = yearChartValues;
            ReportChart.AxisX[0].Labels = yearList;
        }

        private void UpdateChartByDay(List<Booking> filteredBookings)
        {
            // Lấy ngày bắt đầu và kết thúc từ DatePicker
            var startDate = StartDatePicker.SelectedDate;
            var endDate = EndDatePicker.SelectedDate;

            // Kiểm tra xem ngày bắt đầu và kết thúc đã được chọn hay chưa
            if (startDate == null || endDate == null)
            {
                // Nếu một trong hai ngày chưa được chọn, không cập nhật biểu đồ
                return;
            }

            // Lọc các đặt vé dựa trên ngày bắt đầu và kết thúc
            var filteredByDateRange = filteredBookings.Where(b => b.BookingDate >= startDate && b.BookingDate <= endDate).ToList();

            // Lọc theo phim
            if (selectedMovie != "All Movies" && !string.IsNullOrEmpty(selectedMovie))
            {
                filteredByDateRange = filteredByDateRange.Where(b => b.Movie == selectedMovie).ToList();
            }

            // Tạo danh sách các ngày từ ngày bắt đầu đến ngày kết thúc
            var dateRange = new List<DateTime>();
            for (var date = startDate.Value.Date; date <= endDate.Value.Date; date = date.AddDays(1))
            {
                dateRange.Add(date);
            }

            // Tạo biến để lưu trữ số lượng đặt vé cho mỗi ngày trong phạm vi ngày được chọn
            var bookingCounts = new Dictionary<DateTime, int>();

            // Đếm số lượng đặt vé cho từng ngày trong phạm vi ngày được chọn
            foreach (var date in dateRange)
            {
                var count = filteredByDateRange.Count(b => b.BookingDate.Date == date.Date);
                bookingCounts.Add(date, count);
            }

            // Tạo dữ liệu cho biểu đồ
            var dayChartValues = new ChartValues<ObservablePoint>();
            foreach (var kvp in bookingCounts)
            {
                dayChartValues.Add(new ObservablePoint(dayChartValues.Count + 1, kvp.Value));
            }

    // Cập nhật dữ liệu và nhãn trục x của biểu đồ
    (ReportChart.Series[0] as LineSeries).Values = dayChartValues;
            ReportChart.AxisX[0].Labels = dateRange.Select(date => date.ToString("dd/MM/yyyy")).ToList();
        }

        private void UpdateChartByWeek(List<Booking> filteredBookings)
        {
            // Lấy ngày bắt đầu của tuần hiện tại (ngày đầu tiên của tuần)
            var startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            // Lấy ngày kết thúc của tuần hiện tại (ngày cuối cùng của tuần)
            var endDate = startDate.AddDays(6);

            // Lọc các đặt vé dựa trên ngày bắt đầu và kết thúc của tuần
            var filteredByWeek = filteredBookings
                .Where(b => b.BookingDate.Date >= startDate && b.BookingDate.Date <= endDate)
                .ToList();

            // Lọc theo phim nếu cần
            if (selectedMovie != "All Movies" && !string.IsNullOrEmpty(selectedMovie))
            {
                filteredByWeek = filteredByWeek.Where(b => b.Movie == selectedMovie).ToList();
            }

            // Tạo danh sách các ngày trong tuần
            var daysOfWeek = Enumerable.Range(0, 7).Select(d => startDate.AddDays(d)).ToList();

            // Tạo một từ điển để lưu trữ số lượng đặt vé cho mỗi ngày trong tuần
            var bookingCounts = new Dictionary<DateTime, int>();

            // Đếm số lượng đặt vé cho mỗi ngày trong tuần
            foreach (var day in daysOfWeek)
            {
                var count = filteredByWeek.Count(b => b.BookingDate.Date == day.Date);
                bookingCounts.Add(day, count);
            }

            // Tạo dữ liệu cho biểu đồ
            var weekChartValues = new ChartValues<ObservablePoint>();
            foreach (var kvp in bookingCounts)
            {
                weekChartValues.Add(new ObservablePoint(weekChartValues.Count + 1, kvp.Value));
            }

    // Cập nhật dữ liệu và nhãn trục x của biểu đồ
    (ReportChart.Series[0] as LineSeries).Values = weekChartValues;
            ReportChart.AxisX[0].Labels = daysOfWeek.Select(date => date.ToString("dd/MM/yyyy")).ToList();
        }


    }

}
