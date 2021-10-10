using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PageSwiper.Tasks
{
    public partial class Task_2_2_2 : Page
    {

        private string _condition = "Дано натуральное число. Определить, верно ли,\nчто произведение его цифр меньше A, а само число делится на B?";
        private uint _value, _a, _b, _dotProduct = 1;
        private bool _valueEntered, _aEntered, _bEntered;

        public Task_2_2_2()
        {
            InitializeComponent();
        }

        private void InputValue_Changed(object sender, TextChangedEventArgs e)
        {
            if(uint.TryParse(ValueBox.Text, out _value))
            {
                _valueEntered = true;
                ValueBox.Background = Brushes.Gray;
            }
            else
            {
                _valueEntered = false;
                ValueBox.Background = Brushes.Red;
            }
            MakeConclusion();
        }

        private void AValue_Changed(object sender, TextChangedEventArgs e)
        {
            if (uint.TryParse(ABox.Text, out _a))
            {
                _aEntered = true;
                ABox.Background = Brushes.Gray;
            }
            else
            {
                _aEntered = false;
                ABox.Background = Brushes.Red;
            }
            MakeConclusion();
        }

        private void BValue_Changed(object sender, TextChangedEventArgs e)
        {
            if (uint.TryParse(BBox.Text, out _b))
            {
                _bEntered = true;
                BBox.Background = Brushes.Gray;
            }
            else
            {
                _bEntered = false;
                BBox.Background = Brushes.Red;
            }
            MakeConclusion();
        }

        private void MakeConclusion()
        {
            if (_valueEntered && _aEntered && _bEntered)
            {
                foreach (var num in DecomposeValue(_value))
                {
                    _dotProduct *= num;
                }

                if (_dotProduct < _a && _value % _b == 0)
                {
                    OutputBlock.Text = "Произведение цифр числа меньше чем значение А и введённое значение делится нацело на B";
                }
                else if (_dotProduct >= _a)
                {
                    OutputBlock.Text = "Произведение цифр числа больше (или равно) чем значение А";
                }
                else
                {
                    OutputBlock.Text = "Введённое значение не делится нацело на B";
                }
            }
            else OutputBlock.Text = String.Empty;
        }

        private byte[] DecomposeValue(uint value)
        { 
            byte[] decomposedValue = new byte[value.ToString().Length];
            int i = 0;
            while (value > 0)
            {
                decomposedValue[i] = (byte)(value % 10);
                value /= 10;
                ++i;
            }
            return decomposedValue;
        }

        private void ShowCondition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_condition, "Условие");
        }
    }
}
