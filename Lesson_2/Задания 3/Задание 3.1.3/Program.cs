// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 3.1.3 - 8е задание
// Дан текст (строка), содержащий в себе группы  букв, цифр, символов.
// Преобразовать текст, отсортировав каждую группу букв по алфавиту,
// каждую группу цифр в порядке убывания. Например: «cba1076/’abfc3785,’’3946f»  - «abc7610/’abcf8753,’’9643f».
// Не использовать строковые функции

using System;
using static System.Console;

namespace Task_3_1_3
{

    class EntryPoint
    {

        public static void Main()
        {
            Console.WriteLine("Enter a string to be transformed:"); 
            string sourceStr = Console.ReadLine(); // Получаем строку для преобразования
            WriteLine("Transformed string:");
            Transform(sourceStr.ToCharArray());    // Выполняем преобразование, вызовом метода Transform

            Console.ReadKey();
        }

        private static void Transform(char[] source)    // Метод преобразования строки по заданому условию
        {   
            CharType groupType = GetCharType(source[0]);                // Определяем первый тип группы, как значение первого символа в строке
            char[] arrToSort, output = new char[source.Length + 1];     // Инициализируем массив группы (который будет отсортирован) и результирующий массив
            int firstGroupIndex = 0;                                    // Первый индекс группы - будет определять индекс с которого началась группа
            for (int i = 0; i <= source.Length; i++)                    // Проходимся по всем элементам входного массива + 1
            {
                if (i == source.Length) // Условие для последнего элемента массива
                {
                    arrToSort = new char[i - firstGroupIndex];          // Выполняем все действия что и в блоке else if ниже, только при выполнении всех инструкций в этом блоке выходим из цикла for
                    Array.Copy(source, firstGroupIndex, arrToSort, 0, i - firstGroupIndex); 
                    if (groupType != CharType.Symbol)
                        SortArray(ref arrToSort, groupType == CharType.Digit ? false : true); 
                    Array.Copy(arrToSort, 0, output, firstGroupIndex, arrToSort.Length);
                    break;
                }
                else if (groupType != GetCharType(source[i]))           // Условие вхождения в новую группу символов
                {
                    arrToSort = new char[i - firstGroupIndex];          // Инициализируем массив сортировки длинной -> текущий индекс - первый индекс группы
                    Array.Copy(source, firstGroupIndex, arrToSort, 0, i - firstGroupIndex); // Копируем элементы входного массива в массив для сортировки
                    if (groupType != CharType.Symbol)                   // Если текущая группа символов не является группой цифр или букв -> выполняем сортировку
                        SortArray(ref arrToSort, groupType == CharType.Digit ? false : true); // Второй агрумент вычисляем тернарным оператором (Если группа цифр -> сортировка по спаданию, если группа букв -> сортировка в алфавитном порядке)
                    Array.Copy(arrToSort, 0, output, firstGroupIndex, arrToSort.Length); // Отсортированный массив присоединяем к результирующему массиву
                    firstGroupIndex = i; // Переназначаем первый индекс группы, поскольку выполнелось вхождение в новую группу
                    groupType = GetCharType(source[i]); // Переназначаем тип группы
                }
            }
            WriteLine(output);
        }

        private static void SortArray(ref char[] arr, bool ascendingOrder = true)   // Метод сортировки пузырьком
        {                                                                           // В качестве второго аргумента принимает 
            for (int i = 0; i < arr.Length - 1; i++)                                // булевое значение, которое отвечает 
            {                                                                       // за сортировку по спаданию или возрастанию
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (ascendingOrder && arr[j] > arr[j + 1])                      // Если флаг ascendingOrder == true -> сортировка по возрастанию (для буквенных символов)
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                    }
                    if (!ascendingOrder && arr[j] < arr[j + 1])                     // Если флаг ascendingOrder == false -> сортировка по убыванию (для символов цифр)
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                    }
                }
            }
        }

        private static void Swap(ref char a, ref char b)    // Метод перестановки значений двух переменных 
        {
            char temp = a;
            a = b;
            b = temp;
        }

        private static CharType GetCharType(char ch)    // Метод, который возвращает тип символа по аргументу 
        {
            if (Char.IsLetter(ch))
                return CharType.Letter;
            else if (Char.IsDigit(ch))
                return CharType.Digit;
            else if (!Char.IsLetterOrDigit(ch))
                return CharType.Symbol;
            return default(CharType);
        }

        public enum CharType    // Перечисление, обозначающеее тип символа строки
        {
            Letter, Digit, Symbol
        }
    }
}