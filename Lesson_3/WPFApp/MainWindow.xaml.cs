using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PageSwiper
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.TaskFrame.Source = new Uri(@"Tasks\Task_" + (sender as Button).Tag.ToString().Replace('.', '_') + ".xaml", UriKind.RelativeOrAbsolute);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeApp(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
