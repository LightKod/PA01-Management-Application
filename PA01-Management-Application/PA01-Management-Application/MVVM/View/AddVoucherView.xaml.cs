using PA01_Management_Application.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddVoucherView.xaml
    /// </summary>
    public partial class AddVoucherView : Window
    {
        public Voucher newVoucher { get; set; }
        public AddVoucherView()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }
        private void cancelFormBtn_Click(object sender, RoutedEventArgs e)
        {
            // Đóng màn hình nhập liệu
            Close(); // Trả về null để chỉ ra rằng không có voucher nào được thêm mới
        }
        private void saveFormBtn_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra thông tin nhập liệu
            if (IsInputValid())
            {
                // Tạo đối tượng Voucher từ thông tin nhập liệu
                newVoucher = new Voucher
                {
                    VoucherCode = FilmNameBox.Text,
                    VoucherValue = double.Parse(FilmDurationBox.Text) // Bạn cần xử lý chuyển đổi dữ liệu ở đây theo định dạng mong muốn
                };
                this.DialogResult = true;
                // Đóng màn hình nhập liệu và trả về voucher mới tạo
                Close();
            }
            else
            {
                // Hiển thị thông báo lỗi cho người dùng
                MessageBox.Show("Invalid input! Please check your input and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsInputValid()
        {
            // Kiểm tra tính hợp lệ của thông tin nhập liệu ở đây
            // Trong ví dụ này, bạn có thể kiểm tra xem các ô nhập liệu có được điền đầy đủ không, 
            // và xem liệu giá trị nhập vào ô Duration có phải là một số không
            // Đây chỉ là một ví dụ, bạn có thể thực hiện kiểm tra phù hợp với yêu cầu của mình
            if (string.IsNullOrWhiteSpace(FilmNameBox.Text) || !double.TryParse(FilmDurationBox.Text, out _))
            {
                return false;
            }
            return true;
        }
    }
}
