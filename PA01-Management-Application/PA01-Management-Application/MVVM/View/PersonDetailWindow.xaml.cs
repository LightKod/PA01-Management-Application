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
using System.Windows.Shapes;

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for PersonDetailWindow.xaml
    /// </summary>
    public partial class PersonDetailWindow : Window
    {
        Person Person { get; set; }
        public PersonDetailWindow(Person person)
        {
            InitializeComponent();
            Person = person;
            DataContext = Person;
        }
    }
}
