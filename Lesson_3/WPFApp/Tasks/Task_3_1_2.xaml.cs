using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PageSwiper.Tasks
{
    public partial class Task_3_1_2 : Page
    {

        private string _condition = "Даны два неубывающих массива x и y. Найти их соединение, то есть неубывающий массив z, содержащий все их элементы, причем каждый элемент должен входить в массив z столько раз, сколько он входит в общей сложности в массивы x и y.";
        private int[] _arrayOne, _arrayTwo;

        public Task_3_1_2()
        {
            InitializeComponent();
        }

        private void InputValueOne_Changed(object sender, TextChangedEventArgs e)
        {
            FirstArrayPanel.Text = string.Empty;
            var textBox = sender as TextBox;
            if(int.TryParse(textBox.Text, out int len))
            {
                textBox.Background = Brushes.Gray;
                _arrayOne = new int[len];
                _arrayOne.FillRandom().WriteTo(FirstArrayPanel);
            }
            else
            {
                textBox.Background = Brushes.Red;
                OutputArrayPanel.Text = string.Empty;
            }
            MakeConclusion();
        }

        private void InputValueTwo_Changed(object sender, TextChangedEventArgs e)
        {
            SecondArrayPanel.Text = string.Empty;
            var textBox = sender as TextBox;
            if (int.TryParse(textBox.Text, out int len))
            {
                textBox.Background = Brushes.Gray;
                _arrayTwo = new int[len];
                _arrayTwo.FillRandom().WriteTo(SecondArrayPanel);
            }
            else
            {
                textBox.Background = Brushes.Red;
                OutputArrayPanel.Text = string.Empty;
            }
            MakeConclusion();
        }

        private void MakeConclusion()
        {
            OutputArrayPanel.Text = string.Empty;
            if(_arrayOne != null && _arrayTwo != null)
            {
                int[] pooledArray = Concat(_arrayOne, _arrayTwo).SortArray();
                pooledArray.WriteTo(OutputArrayPanel);
            }
        }

        private static int[] Concat(int[] arr1, int[] arr2)
        {
            int[] pooledArray = new int[arr1.Length + arr2.Length];
            for (int i = 0; i < arr1.Length; i++)
            {
                pooledArray[i] = arr1[i];
            }
            for (int i = 0, pooledIter = arr1.Length; i < arr2.Length; i++, pooledIter++)
            {
                pooledArray[pooledIter] = arr2[i];
            }

            return pooledArray;
        }

        private void ShowCondition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_condition, "Условие");
        }
    }

    public static partial class Extension   
    {

        public static int[] SortArray(this int[] array)             
        {
            for (int i = 1; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i; j++)
                {
                    if (array[j] > array[j + 1])
                        Swap(ref array[j], ref array[j + 1]);
                }
            }
            return array;
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}
