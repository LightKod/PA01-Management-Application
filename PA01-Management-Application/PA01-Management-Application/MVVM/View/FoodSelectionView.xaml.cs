﻿using PA01_Management_Application.MVVM.ViewModel;
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
    /// Interaction logic for FoodSelectionView.xaml
    /// </summary>
    public partial class FoodSelectionView : UserControl
    {
        FoodSelectionViewModel viewModel;

        public FoodSelectionView()
        {
            InitializeComponent();
            viewModel = new FoodSelectionViewModel();
            DataContext = viewModel;

        }
    }
}
