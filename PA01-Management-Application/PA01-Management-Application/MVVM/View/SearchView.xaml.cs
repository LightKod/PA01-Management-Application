using PA01_Management_Application.MVVM.ViewModel;
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
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
        }

        private void advancedSearchToogle_Click(object sender, RoutedEventArgs e)
        {
            if (advancedSearchBox.Visibility == Visibility.Visible)
            {
                advancedSearchBox.Visibility = Visibility.Collapsed;
                advancedSearchToogle.Content = "Advanced Search";
                searchBtn.Visibility = Visibility.Visible;
            }
            else
            {
                advancedSearchToogle.Content = "Close Advanced Search";
                advancedSearchBox.Visibility = Visibility.Visible;
                searchBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void advancedSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(Validation.GetHasError(TimeBox) || Validation.GetHasError(PriceBox) || Validation.GetHasError(YearBox)))
            {
                if (DataContext is SearchViewModel viewModel)
                {
                    if (viewModel.AdvancedSearchCommand.CanExecute(this))
                    {
                        viewModel.AdvancedSearchCommand.Execute(this);
                    }
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is SearchViewModel viewModel)
            {
                if (viewModel.SortCommand.CanExecute(this))
                {
                    viewModel.SortCommand.Execute(this);
                }
            }
        }
    }
}
