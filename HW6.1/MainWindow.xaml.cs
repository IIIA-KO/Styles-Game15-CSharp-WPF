using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW6
{
    public partial class MainWindow : Window
    {
        private Game15 Game;
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();

            Game = new Game15();
            StartGame();
        }

        private void StartGame()
        {
            try
            {
                MainGrid.Children.Add(CreateButton("1", Brushes.Cornsilk, Brushes.IndianRed));
                MainGrid.Children.Add(CreateButton("2", Brushes.IndianRed, Brushes.Cornsilk));
                MainGrid.Children.Add(CreateButton("3", Brushes.Cornsilk, Brushes.IndianRed));
                MainGrid.Children.Add(CreateButton("4", Brushes.IndianRed, Brushes.Cornsilk));
                MainGrid.Children.Add(CreateButton("5", Brushes.IndianRed, Brushes.Cornsilk));
                MainGrid.Children.Add(CreateButton("6", Brushes.Cornsilk, Brushes.IndianRed));
                MainGrid.Children.Add(CreateButton("7", Brushes.IndianRed, Brushes.Cornsilk));
                MainGrid.Children.Add(CreateButton("8", Brushes.Cornsilk, Brushes.IndianRed));
                MainGrid.Children.Add(CreateButton("9", Brushes.Cornsilk, Brushes.IndianRed));
                MainGrid.Children.Add(CreateButton("10", Brushes.IndianRed, Brushes.Cornsilk));
                MainGrid.Children.Add(CreateButton("11", Brushes.Cornsilk, Brushes.IndianRed));
                MainGrid.Children.Add(CreateButton("12", Brushes.IndianRed, Brushes.Cornsilk));
                MainGrid.Children.Add(CreateButton("13", Brushes.IndianRed, Brushes.Cornsilk));
                MainGrid.Children.Add(CreateButton("14", Brushes.Cornsilk, Brushes.IndianRed));
                MainGrid.Children.Add(CreateButton("15", Brushes.IndianRed, Brushes.Cornsilk));

                MainGrid.Children.Add(CreateButton("0", Brushes.IndianRed, Brushes.Cornsilk));

                Game.Start();

                for (int i = 0; i < 100; i++)
                    Game.RandomMove();

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Button CreateButton(string content, SolidColorBrush foreground, SolidColorBrush backgroung)
        {
            try
            {
                Button button = new Button
                {
                    Content = content,
                    Tag = Convert.ToInt32(content) == 0 ? 16 : Convert.ToInt32(content) - 1,
                    Foreground = foreground,
                    Background = backgroung,
                    Style = (Style)this.TryFindResource("BtnStyle")
                };

                button.Visibility = Convert.ToInt32(content) == 0 ? Visibility.Hidden : Visibility.Visible;

                int row, column;
                Game.TagToCoordinates(Convert.ToInt32(content), out row, out column);

                Grid.SetRow(button, row);
                Grid.SetColumn(button, column);

                button.Click += Button_Click;

                return button;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
                return new Button();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tag = Convert.ToInt32(((Button)sender).Tag);
                Game.Move(tag);

                Refresh();

                if (Game.IsGameOver())
                {
                    MessageBox.Show("Вітаємо, ви пройшли гру !", "Привітання", MessageBoxButton.OK, MessageBoxImage.Stop);
                    StartGame();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Refresh()
        {
            try
            {
                for (int i = 0; i < 16; i++)
                {
                    int tag = Game.GetTag(i);

                    ((Button)MainGrid.Children[i]).Content = tag;

                    if (tag > 0)
                        ((Button)MainGrid.Children[i]).Visibility = Visibility.Visible;
                    else
                        ((Button)MainGrid.Children[i]).Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
