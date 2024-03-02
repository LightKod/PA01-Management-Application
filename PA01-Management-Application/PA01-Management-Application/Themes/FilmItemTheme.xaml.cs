using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows;
using PA01_Management_Application.Core;

namespace PA01_Management_Application.Themes
{
    /// <summary>
    /// Class to handle events for FilmItemTheme.xaml ResourceDictionary
    /// </summary>
    partial class FilmItemTheme : ResourceDictionary
    {
        public FilmItemTheme()
        {
            InitializeComponent();
        }

        private void border_MouseEnter(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            var BorderWidthAnimation = new DoubleAnimation()
            {
                To = Constants.sliderItemVideoWidth,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase()
            };

            border.BeginAnimation(Border.WidthProperty, BorderWidthAnimation);

            var grid = border.Child as Grid;
            foreach (var child in grid.Children)
            {
                if (child is MediaElement mediaElement)
                {
                    mediaElement.Play();
                }
                else if (child is Image image)
                {
                    var OpacityAnimation = new DoubleAnimation()
                    {
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.3),
                        EasingFunction = new CubicEase()
                    };
                    image.BeginAnimation(Image.OpacityProperty, OpacityAnimation);
                }
                else if (child is StackPanel stackPanel)
                {
                    var OpacityAnimation = new DoubleAnimation()
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.3),
                        EasingFunction = new CubicEase()
                    };
                    stackPanel.BeginAnimation(StackPanel.OpacityProperty, OpacityAnimation);
                }
            }
        }

        private void border_MouseLeave(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            var BorderWidthAnimation = new DoubleAnimation()
            {
                To = 150,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase()
            };

            border.BeginAnimation(Border.WidthProperty, BorderWidthAnimation);

            var grid = border.Child as Grid;
            foreach (var child in grid.Children)
            {
                if (child is MediaElement mediaElement)
                {
                    mediaElement.Stop();
                }
                else if (child is Image image)
                {
                    var OpacityAnimation = new DoubleAnimation()
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.3),
                        EasingFunction = new CubicEase()
                    };
                    image.BeginAnimation(Image.OpacityProperty, OpacityAnimation);
                }
                else if (child is StackPanel stackPanel)
                {
                    var OpacityAnimation = new DoubleAnimation()
                    {
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.3),
                        EasingFunction = new CubicEase()
                    };
                    stackPanel.BeginAnimation(StackPanel.OpacityProperty, OpacityAnimation);
                }
            }
        }
    }
}
