using System;
using System.Windows;
using System.Windows.Controls;

namespace PageSwiper.Tasks
{
    /// <summary>
    /// Логика взаимодействия для Task_3_1_3.xaml
    /// </summary>
    public partial class Task_3_1_3 : Page
    {

        private string _condition = "Дан текст (строка), содержащий в себе группы  букв, цифр, символов. Преобразовать текст, отсортировав каждую группу букв по алфавиту, каждую группу цифр в порядке убывания. Например: «cba1076/’abfc3785,’’3946f»-«abc7610/’abcf8753,’’9643f». Не использовать строковые функции";

        public Task_3_1_3()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OutputBlock.Text = string.Empty;
            Transform((sender as TextBox).Text.ToCharArray());
        }

        private void Transform(char[] source)
        {
            if(source.Length > 0)
            {
                CharType groupType = GetCharType(source[0]);
                char[] arrToSort, output = new char[source.Length + 1];
                int firstGroupIndex = 0;
                for (int i = 0; i <= source.Length; i++)
                {
                    if (i == source.Length)
                    {
                        arrToSort = new char[i - firstGroupIndex];
                        Array.Copy(source, firstGroupIndex, arrToSort, 0, i - firstGroupIndex);
                        if (groupType != CharType.Symbol)
                            SortArray(ref arrToSort, groupType == CharType.Digit ? false : true);
                        Array.Copy(arrToSort, 0, output, firstGroupIndex, arrToSort.Length);
                        break;
                    }
                    else if (groupType != GetCharType(source[i]))
                    {
                        arrToSort = new char[i - firstGroupIndex];
                        Array.Copy(source, firstGroupIndex, arrToSort, 0, i - firstGroupIndex);
                        if (groupType != CharType.Symbol)
                            SortArray(ref arrToSort, groupType == CharType.Digit ? false : true);
                        Array.Copy(arrToSort, 0, output, firstGroupIndex, arrToSort.Length);
                        firstGroupIndex = i;
                        groupType = GetCharType(source[i]);
                    }
                }
                for (int i = 0; i < output.Length - 1; i++)
                {
                    OutputBlock.Text += output[i];
                }
            }
        }

        private void SortArray(ref char[] arr, bool ascendingOrder = true)   
        {                                                                           
            for (int i = 0; i < arr.Length - 1; i++)                                
            {                                                                       
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (ascendingOrder && arr[j] > arr[j + 1])                      
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                    }
                    if (!ascendingOrder && arr[j] < arr[j + 1])                     
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                    }
                }
            }
        }

        private void Swap(ref char a, ref char b)    
        {
            char temp = a;
            a = b;
            b = temp;
        }

        private CharType GetCharType(char ch)   
        {
            if (Char.IsLetter(ch))
                return CharType.Letter;
            else if (Char.IsDigit(ch))
                return CharType.Digit;
            else if (!Char.IsLetterOrDigit(ch))
                return CharType.Symbol;
            return default(CharType);
        }

        public enum CharType    
        {
            Letter, Digit, Symbol
        }

        private void ShowCondition(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_condition, "Условие");
        }
    }
}
