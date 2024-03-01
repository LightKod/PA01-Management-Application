using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using PA01_Management_Application.Core;

namespace PA01_Management_Application.Themes
{
    /// <summary>
    /// Class to handle events for FilmSetTheme.xaml ResourceDictionary
    /// </summary>
    partial class FilmSetTheme : ResourceDictionary
    {
        public FilmSetTheme()
        {
            InitializeComponent();
        }

        private double CountSliderTotalLength(ItemsControl itemsControl)
        {
            double totalLength = 0;

            foreach (var item in itemsControl.Items)
            {
                var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as UIElement;

                if (container is FrameworkElement element)
                {
                    totalLength += element.ActualWidth;
                }
            }

            return totalLength;
        }

        private void MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var button = sender as Button;

            var OpacityAnimation = new DoubleAnimation()
            {
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase()
            };
            button.BeginAnimation(Button.OpacityProperty, OpacityAnimation);
        }

        private void MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var button = sender as Button;
            var OpacityAnimation = new DoubleAnimation()
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase()
            };
            button.BeginAnimation(Button.OpacityProperty, OpacityAnimation);
        }

        private void PrevButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var grid = button.Parent as Grid;

            if (grid.FindName("itemsControl") is ItemsControl itemsControl)
            {
                if (itemsControl.Margin.Left >= Constants.initialSliderMarginLeft)
                {
                    return;
                }

                Thickness oldThickness = itemsControl.Margin;
                ThicknessAnimation animation = new ThicknessAnimation()
                {
                    To = new Thickness(oldThickness.Left + Constants.sliderItemWidth, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(0.3),
                    EasingFunction = new QuadraticEase()
                };
                itemsControl.BeginAnimation(FrameworkElement.MarginProperty, animation);
            }
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var grid = button.Parent as Grid;
            var stackpanel = grid.Parent as StackPanel;
            var textblock = stackpanel.FindName("textBlock") as TextBlock;

            if (grid.FindName("itemsControl") is ItemsControl itemsControl)
            {
                if (CountSliderTotalLength(itemsControl) - Constants.sliderItemWidth + Constants.sliderItemVideoWidth <= itemsControl.ActualWidth)
                {
                    return;
                }

                Thickness oldThickness = itemsControl.Margin;
                ThicknessAnimation animation = new ThicknessAnimation()
                {
                    To = new Thickness(oldThickness.Left - Constants.sliderItemWidth, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(0.3),
                    EasingFunction = new QuadraticEase()
                };
                itemsControl.BeginAnimation(FrameworkElement.MarginProperty, animation);
            }
        }
    }
}
