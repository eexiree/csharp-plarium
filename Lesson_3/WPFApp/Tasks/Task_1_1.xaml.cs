using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PageSwiper.Tasks
{
    public partial class Task_1_1 : Page
    {
        #region Condition
        private string _condition = "Написать программу, которая в цикле введет 8 значений и посчитает сумму чисел, которые являются квадратами целых чисел (квадратный корень из которых – целое число).";
        #endregion
        
        private TextBox[] _textBoxes;

        public Task_1_1()
        {
            InitializeComponent();
            
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _textBoxes = new TextBox[8] { 
                TB0,
                TB1,
                TB2,
                TB3,
                TB4,
                TB5,
                TB6,
                TB7
            };

        }

        private void InputValue_Changed(object sender, RoutedEventArgs e)
        {
            int sum = 0;
            foreach(var tb in _textBoxes)
            {
                if (int.TryParse(tb.Text, out int inputValue))
                {
                    if (IsFullSqrt(inputValue))
                        sum += inputValue;
                    this.Sum.Text = "Sum is: " + sum.ToString();
                    tb.Background = Brushes.Gray;
                }
                else if(tb.Text != string.Empty)
                {
                    tb.Background = Brushes.Red;
                }
            }
        }

        private bool IsFullSqrt(int val)
        {
            int temp = (int)Math.Sqrt(val);         
            return temp * temp == val ? true : false;
        }

        private void ShowCondition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_condition, "Условие");
        }
    }
}
