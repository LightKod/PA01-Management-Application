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
    /// Interaction logic for VoucherView.xaml
    /// </summary>
    public partial class VoucherView : UserControl
    {
        public VoucherView()
        {
            InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MVVM.View.AccountPageView accountView = new MVVM.View.AccountPageView();

            (App.Current.MainWindow.DataContext as MVVM.ViewModel.AppWindowViewModel).CurrentView = accountView;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
