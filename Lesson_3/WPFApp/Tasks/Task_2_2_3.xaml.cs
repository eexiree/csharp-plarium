using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PageSwiper.Tasks
{
    public partial class Task_2_2_3 : Page
    {

        private string _condition = "Даны натуральные числа M и N. Получить все натуральные числа, меньшие N, квадрат суммы цифр которых равен M.";
        private uint _m, _n;
        private bool _mEntered, _nEntered;

        public Task_2_2_3()
        {
            InitializeComponent();
        }

        private void MBoxValue_Changed(object sender, TextChangedEventArgs e)
        {
            OutputPanel.Text = string.Empty;
            if (uint.TryParse(MBox.Text, out _m))
            {
                _mEntered = true;
                MBox.Background = Brushes.Gray;
            }
            else
            {
                _mEntered = false;
                MBox.Background = Brushes.Red;
            }
            MakeConclusion();
        }

        private void NBoxValue_Changed(object sender, TextChangedEventArgs e)
        {
            OutputPanel.Text = string.Empty;
            if (uint.TryParse(NBox.Text, out _n))
            {
                _nEntered = true;
                NBox.Background = Brushes.Gray;
            }
            else
            {
                _nEntered = false;
                NBox.Background = Brushes.Red;
            }
            MakeConclusion();
        }

        private void MakeConclusion()
        {
            if (_mEntered && _nEntered)
            {
                for (uint i = 0; i < _n; i++)
                {
                    if (_m == SquareSum(DecomposeNumber(i)))
                    {
                        OutputPanel.Text += i.ToString() + " / ";
                    }
                    else continue;
                }
            }
            else OutputPanel.Text = string.Empty;
        }

        private byte[] DecomposeNumber(uint number)
        {
            byte[] decomposedValue = new byte[number.ToString().Length];
            int i = 0;
            while (number > 0)
            {
                decomposedValue[i] = (byte)(number % 10);
                number /= 10;
                ++i;
            }
            return decomposedValue;
        }

        private uint SquareSum(byte[] decomposedNumber)  
        {
            uint sum = 0;
            for (int i = 0; i < decomposedNumber.Length; i++)
            {
                sum += decomposedNumber[i];
            }
            return (uint)Math.Pow(sum, 2.0);    
        }

        private void ShowCondition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_condition, "Условие");
        }

    }
}
