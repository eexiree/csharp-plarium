// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 1.1 - 8е задание
// Написать программу, которая в цикле введет 8 значений
// и посчитает сумму чисел, которые являются квадратами
// целых чисел (квадратный корень из которых – целое число).

using System;

namespace Task_1_1
{
    public class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("Enter 8 values:");
            int sum = 0, inputValue; // Переменные для хранения суммы и входного значения
            for (int i = 0; i < 8; i++)
            {
                Console.Write("#" + (i + 1).ToString() + " ");
                if (!Int32.TryParse(Console.ReadLine(), out inputValue)) // Проверка входного значения
                {
                    Console.WriteLine("Entered value is not integer");
		            Console.ReadKey();
                    return;
                }
                if (IsFullSqrt(inputValue)) // Проверяем, является ли введённое число полным квадратом целого числа, если да -> добавляем его к переменной суммы
                {
                    sum += inputValue;
                }
            }
            Console.Write($"Sum of full sqrt integers is: {sum}"); // Выводим значение суммы 
            Console.ReadKey();
        }

        private static bool IsFullSqrt(int val)
        {
            int temp = (int)Math.Sqrt(val);             // Рассчитываем значение корня от входного числа
            return temp * temp == val ? true : false;   // Если обратное возведение в квадрат даст входное значение, то входное число является полным квадратом целого числа
        }

    }
}
