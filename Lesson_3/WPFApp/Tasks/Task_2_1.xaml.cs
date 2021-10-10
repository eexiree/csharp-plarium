using System.Windows;
using System.Windows.Controls;

namespace PageSwiper.Tasks
{
    public partial class Task_2_1 : Page
    {

        private string _condition = "Напечатать в возрастающем порядке все трехзначные числа,\nв десятичной записи которых нет одинаковых цифр.\nОперации деления, целочисленного деления и определения остатка не использовать.";

        public Task_2_1()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 100; i < 1000; i++)
            {
                if (ConsistUniqueNumerals(i))
                {
                    this.OutputPanel.Text += i.ToString() + " / ";
                }
            }
        }

        private static bool ConsistUniqueNumerals(int value)
        {
            string strRep = value.ToString();
            return strRep[0] != strRep[1] &&
                   strRep[0] != strRep[2] &&
                   strRep[1] != strRep[2] ? true : false;
        }

        private void ShowCondition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_condition, "Условие");
        }
    }
}
