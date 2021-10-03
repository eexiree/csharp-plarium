// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 3.1.1 - 8е задание
// Из одномерного целочисленного массива переписать все числа во второй массив так,
// чтобы сначала шли четные элементы, затем нули, потом нечетные элементы

using System;

namespace Task_3_1_1 {

    public class EntryPoint {

        public static void Main() {

            Console.WriteLine("Enter the length of array:");
            int arrayLength;
            if (!Int32.TryParse(Console.ReadLine(), out arrayLength))   // Проверка входных данных
            {
                Console.WriteLine("Entered value is not an integer");
                return;
            }

            int[] sourceArray = new int[arrayLength];       // Исходный массив
            int[] rewritedArray = new int[arrayLength];     // Результирующий массив

            sourceArray.FillRandom();                       // Заполняем исходный массив случайными числами, с помощью extension method
            rewritedArray.Rewrite(sourceArray);             // Переписываем числа из исходного массива в результирующий также с помощью extension method

            Console.WriteLine("\nSource array:");
            foreach (var item in sourceArray)               // Выводим исходный массив
            {
                Console.Write(item + "\t");
            }

            Console.WriteLine("\nRewrited array:");
            foreach (var item in rewritedArray)             // Выводим результирующий массив
            {
                Console.Write(item + "\t");
            }

            Console.ReadKey();
        }
    }

    public static class Extension   // Публичный класс расширенных методов
    {
        public static int[] FillRandom(this int[] array)    // Расширяющий метод для заполнения массива случайными числами
        {
            Random rng = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rng.Next(0, 101) - 50;
            }
            return array;
        }

        public static int[] Rewrite(this int[] to, int[] from)  // Расширяющий метод для копирования исходного массива в результирующий по заданому условию
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
    }
}