using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PageSwiper.Tasks
{
    public partial class Task_2_2_1 : Page
    {

        private string _condition = "Дано натуральное число.\nОпределить, все ли цифры в нем одинаковы.";

        public Task_2_2_1()
        {
            InitializeComponent();
        }

        private void InputValue_Changed(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (int.TryParse(textBox.Text, out int inputValue) && inputValue.ToString().Length > 1)
            {
                if(ConsistIdenticalNumerals(inputValue))
                {
                    OutputBlock.Text = "Все цифры одинаковые";
                }
                else OutputBlock.Text = "Цифры не одинаковые";
                textBox.Background = Brushes.Gray;
            }
            else textBox.Background = Brushes.Red;
        }

        private bool ConsistIdenticalNumerals(int value)
        {
            string strRep = value.ToString();
            for (int i = 0; i < strRep.Length - 1; i++)
            {
                if (strRep[i] != strRep[i + 1])
                    return false;
            }
            return true;
        }

        private void ShowCondition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_condition, "Условие");
        }
    }
}
