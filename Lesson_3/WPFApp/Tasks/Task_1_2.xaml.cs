using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PageSwiper.Tasks
{
    public partial class Task_1_2 : Page
    {

        private string _condition = "Дано натуральное число n,\nвычислить sqrt(3 + sqrt(6 + ... + sqrt(3 * n)))";

        public Task_1_2()
        {
            InitializeComponent();
        }

        private void InputValue_Changed(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (int.TryParse(textBox.Text, out int inputValue))
            {
                this.OutputBox.Text = "Result is: " + ComputeValue(inputValue).ToString();
                textBox.Background = Brushes.Gray;
            }
            else textBox.Background = Brushes.Red;
        }

        private double ComputeValue(int n)
        {
            double answer = 0;  
            for (int i = 0; i < n; i++)
                answer = Math.Sqrt((double)(3 * (n - i)) + answer);
            return answer;
        }

        private void ShowCondition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_condition, "Условие");
        }
    }
}
