// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 3.1.2 - 8е задание
// Даны два неубывающих массива x и y.
// Найти их соединение, то есть неубывающий массив z,
// содержащий все их элементы, причем каждый элемент
// должен входить в массив z столько раз, сколько он
// входит в общей сложности в массивы x и y.

using System;

namespace Task_3_1_2
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {

            int array1Length, array2Length;

            Console.WriteLine("Enter a size of both arrays:");
            if (!Int32.TryParse(Console.ReadLine(), out array1Length) || !Int32.TryParse(Console.ReadLine(), out array2Length)) // Проверка входных данных
            {
                Console.WriteLine("One of these entered values contain an unsuported type\nExiting...");
                return;
            }


            int[] array1 = new int[array1Length], array2 = new int[array2Length], pooledArray; // Инициализация массивов
            array1.FillRandom(5);   // Заполнение массива х (название х по условию)
            array2.FillRandom(10);  // Заполнение массива у (название у по условию)

            Console.WriteLine("\nArray 1:");
            foreach (var item in array1)        // Выводим значения элементов массива х
            {
                Console.Write(item + "\t");
            }

            Console.WriteLine("\nArray 2:");    
            foreach (var item in array2)        // Выводим значения элементов массива у
            {
                Console.Write(item + "\t");
            }

            pooledArray = Concat(array1, array2).SortArray();   // Выполняем метод соединения массивов и их сортировку и результат записываем в результирующий массив

            Console.WriteLine("\nPooled, sorted array:");       
            foreach (var item in pooledArray)   // Выводим значения элементов результирующего массива 
            {
                Console.Write(item + "\t");
            }

            Console.ReadKey();
        }

        private static int[] Concat(int[] arr1, int[] arr2) // Метод соединения массивов
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
    }

    public static class Extension   // Публичный класс расширяющих методов для целочисленных массивов
    {

        public static int[] FillRandom(this int[] array, int seed)  // Расширяющий метод для заполнения массива случайными числами в диапазоне от (-50, 50)
        {
            Random rng = new Random(seed);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rng.Next(0, 101) - 50;
            }
            return array;
        }

        public static int[] SortArray(this int[] array)             // Расширяющий метод сортировки массива методом пузырька
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

        private static void Swap(ref int a, ref int b) // Метод, который меняет местами значения двух переменных
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}