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
using System.Windows.Shapes;

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddEditFilmView.xaml
    /// </summary>
    public partial class AddEditFilmView : Window
    {
        public AddEditFilmView()
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

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void genreComboBoxGroupToggle_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditFilmViewModel viewModel)
            {
                if (viewModel.GetGenreCommand.CanExecute(this))
                {
                    viewModel.GetGenreCommand.Execute(this);
                }
            }
            genreComboBoxGroupToggle.Visibility = Visibility.Collapsed;
            genreComboBoxGroup.Visibility = Visibility.Visible;
        }

        private void genreComboBoxGroupCancel_Click(object sender, RoutedEventArgs e)
        {
            genreComboBoxGroup.Visibility = Visibility.Collapsed;
            genreComboBoxGroupToggle.Visibility = Visibility.Visible;
        }

        private void peopleComboBoxGroupToggle_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditFilmViewModel viewModel)
            {
                if (viewModel.GetPeopleCommand.CanExecute(this))
                {
                    viewModel.GetPeopleCommand.Execute(this);
                }
            }
            peopleComboBoxGroupToggle.Visibility = Visibility.Collapsed;
            peopleComboBoxGroup.Visibility = Visibility.Visible;
        }

        private void peopleComboBoxGroupCancel_Click(object sender, RoutedEventArgs e)
        {
            peopleComboBoxGroup.Visibility = Visibility.Collapsed;
            peopleComboBoxGroupToggle.Visibility = Visibility.Visible;
        }

        private void genreComboBoxGroupAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditFilmViewModel viewModel)
            {
                if (viewModel.SaveGenreCommand.CanExecute(genreComboBox.SelectedItem as string))
                {
                    viewModel.SaveGenreCommand.Execute(genreComboBox.SelectedItem as string);
                }
            }
            genreComboBoxGroup.Visibility = Visibility.Collapsed;
            genreComboBoxGroupToggle.Visibility = Visibility.Visible;
        }

        private void peopleComboBoxGroupAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditFilmViewModel viewModel)
            {
                var data = new Tuple<string, string>(personComboBox.SelectedItem as string, roleComboBox.SelectedItem as string);
                if (viewModel.SavePersonCommand.CanExecute(data))
                {
                    viewModel.SavePersonCommand.Execute(data);
                }
            }
            peopleComboBoxGroup.Visibility = Visibility.Collapsed;
            peopleComboBoxGroupToggle.Visibility = Visibility.Visible;
        }

        private void cancelFormBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveFormBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditFilmViewModel viewModel)
            {
                if (viewModel.CheckFormValidityCommand.CanExecute(this))
                {
                    viewModel.CheckFormValidityCommand.Execute(this);
                }

                if (!viewModel.IsValidForm)
                {
                    MessageBox.Show("You might have not completely filled in the form. Please make sure to fill every field in the form correctly before pressing Save.", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Close();
                }
            }
        }
    }
}
