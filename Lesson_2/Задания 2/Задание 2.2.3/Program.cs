// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 2.2.3 - 8е задание 
// Даны натуральные числа m и n.
// Получить все натуральные числа,
// меньшие n, квадрат суммы цифр которых равен m.

using System;
using System.Collections.Generic;

namespace Task_2_2_3
{
    public class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("Enter m and n");
            uint n, m;
            if (!UInt32.TryParse(Console.ReadLine(), out m) || !UInt32.TryParse(Console.ReadLine(), out n)) // Проверка входных данных
            {
                Console.WriteLine("One of these entered values contain unsuported type\nExiting...");
                Console.ReadKey();
                return;
            }


            for (uint i = 0; i < n; i++)    // Проходимся по всем числам которые меньше n
            {
                if (m == SquareSum(DecomposeNumber(i))) // Выполняем проверку, заданную по условию задачи
                    Console.Write(i + " / ");
                else continue;
            }
            Console.ReadKey();
        }

        private static byte[] DecomposeNumber(uint number)  // Метод разбиения числа на составляющие цифры
        {
            List<byte> decomposedValue = new List<byte>();
            while (number > 0)
            {
                decomposedValue.Add((byte)(number % 10));
                number /= 10;
            }
            decomposedValue.Reverse();
            return decomposedValue.ToArray();
        }

        private static uint SquareSum(byte[] decomposedNumber)  // Метод, который вычисляет квадрат суммы цифр, из которого состоит число
        {
            uint sum = 0;
            for (int i = 0; i < decomposedNumber.Length; i++)
            {
                sum += decomposedNumber[i];
            }
            return (uint)Math.Pow(sum, 2.0);    // Возвращаем сумму, возвёденную в квадрат
        }

    }

}