using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PageSwiper.Tasks
{

    public partial class Task_3_1_1 : Page
    {

        private string _condition = "Из одномерного целочисленного массива переписать все числа во второй массив так, чтобы сначала шли четные элементы, затем нули, потом нечетные элементы";
        private int[] _inputArr, _outputArr;

        public Task_3_1_1()
        {
            InitializeComponent();
        }

        private void InputValue_Changed(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            InputArrayPanel.Text = OutputArrayPanel.Text = string.Empty;
            if (int.TryParse(textBox.Text, out int len))
            {
                _inputArr = new int[len];
                _outputArr = new int[len];

                _inputArr.FillRandom();
                _outputArr.Rewrite(_inputArr);

                _inputArr.WriteTo(InputArrayPanel);
                _outputArr.WriteTo(OutputArrayPanel);

                textBox.Background = Brushes.Gray;
            }
            else
            {
                InputArrayPanel.Text = OutputArrayPanel.Text = string.Empty;
                textBox.Background = Brushes.Red;
            }
        }

        private void ShowCondition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_condition, "Условие");
        }
    }

    public static partial class Extension   
    {
        public static int[] FillRandom(this int[] array)    
        {
            Random rng = new Random((Int32)DateTime.Now.Ticks);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rng.Next(0, 101) - 50;
            }
            return array;
        }

        public static int[] Rewrite(this int[] to, int[] from)  
        {
            int lastIndex = 0;

            for (int i = 0; i < from.Length; i++)
            {
                if ((from[i] & 1) == 0 && from[i] != 0 && lastIndex < to.Length)
                {
                    to[lastIndex++] = from[i];
                }
            }
            for (int i = 0; i < from.Length; i++)
            {
                if (from[i] == 0 && lastIndex < to.Length)
                {
                    to[lastIndex++] = from[i];
                }
            }
            for (int i = 0; i < from.Length; i++)
            {
                if ((from[i] & 1) == 1 && lastIndex < to.Length)
                {
                    to[lastIndex++] = from[i];
                }
            }

            return to;
        }

        public static void WriteTo(this int[] data, TextBlock writeTo)
        {
            foreach(var item in data)
            {
                writeTo.Text += item.ToString() + " / ";
            }
        }

    }
}
