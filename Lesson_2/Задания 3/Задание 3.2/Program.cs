// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 3.2 - 8е задание
// Характеристикой столбца целочисленной матрицы назовем сумму модулей его отрицательных нечетных элементов.
// Переставляя столбцы заданной матрицы, расположить их в соответствии с ростом характеристик.
// Найти сумму элементов в тех столбцах, которые содержат хотя бы один отрицательный элемент.


using System;

namespace Task_3_2
{

    public class EntryPoint
    {
        private static int[,] _matrix;          // Статический двумерный массив матрицы
        private static int[] _characteristic;   // Статический массив характеристики матрицы


        public static void Main()
        {
            int matrixSize; // Размер матрицы (Будет применяться квадратная матрица)
            Console.WriteLine("Enter a size of matrix (NxN - type of matrix)");
            if (!Int32.TryParse(Console.ReadLine(), out matrixSize)) // Проверка входных данных 
            {
                Console.WriteLine("Entered value is not an integer\nExiting...");
                return;
            }


            _matrix = FillMatrix(matrixSize, out _characteristic); // Заполняем матрицу случайными числами, и сразу же вычисляем характеристику каждого столбца
            
            Console.WriteLine("Source matrix:");            // Выводим исходную матрицу и её характеристику
            OutputMatrix(_matrix, _characteristic);

            SortMatrix(ref _matrix, ref _characteristic);   // Сортируем столбцы матрицы согласно её характеристики 
            Console.WriteLine("Sorted matrix:");            // Выводим результирующую матрицу и её характеристику
            OutputMatrix(_matrix, _characteristic);

            Console.WriteLine("Columns Sum:");              
            ColumnsSum(_matrix);                            // Вызываем метод, который рассчитает суму элементов столбцов, в которых существует хотя бы один отрицательный элемент


            Console.ReadKey();
        }

        private static int[,] FillMatrix(int size, out int[] characteristics) // Метод заполнения матрицы случайными (псевдослучайными) числами, второй параметр - массив характеристик каждого столбца
        {
            int[,] matrix = new int[size, size];    // Создаем двумерный массив размером size x size
            characteristics = new int[size];        // Создаем массив для хранения характеристики каждого столбца
            Random rng = new Random();              // Создаем экземпляр класса Random, с помощью которого будем генерировать числа

            for (int row = 0; row < size; row++)    // Проходимся по каждому элементу двумерного массива
            {
                for (int col = 0; col < size; col++)
                {
                    matrix[row, col] = rng.Next(0, 11) - 5; // Присваиваем случайное число в диапазоне от -5 до 5
                    if ((row & 1) == 1 && matrix[row, col] < 0) 
                    {
                        characteristics[col] += Math.Abs(matrix[row, col]); // Если элемент имеет нечётный индекс и является отрицательным -> суммируем его модульное значение с определенным элементом в массиве характеристики 
                    }
                }
            }
            return matrix; // Возвращаем заполненый двумерный массив
        }

        private static void ColumnsSum(int[,] matrix)
        {
            int[] columnSum = new int[matrix.GetLength(0)];         // Инициализируем массив в который будем записывать сумму элементов столбцов
            for(int row = 0; row < matrix.GetLength(0); row++)      // Проходимся по каждому из элементов двумерного массива
            {
                for(int col = 0; col < matrix.GetLength(0); col++)
                {
                    if (matrix[col, row] < 0)                       // Если элемент отрицательный -> суммируем элементы столбца и выходим из цикла
                    {
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            columnSum[row] += matrix[i, row];
                        }
                        break;
                    }
                }
            }
            foreach (var item in columnSum) // Выводим суммы столбцов
                Console.Write(item + "\t");
        }

        private static void SortMatrix(ref int[,] matrix, ref int[] characteristic) // Метод сортировки двумерного массива по его характеристике
        {
            for (int iter = 1; iter <= characteristic.Length - 1; iter++)
            {
                for (int col = 0; col < characteristic.Length - iter; col++)
                {
                    if (characteristic[col] > characteristic[col + 1])  // Сортируем по возрастающему значению характеристики
                    {
                        Swap(ref characteristic[col], ref characteristic[col + 1]); // Меняем местами значение элементов массива характеристики
                        for (int row = 0; row < matrix.GetLength(0); row++)         // Меняем местами столбцы 
                        {
                            Swap(ref matrix[row, col], ref matrix[row, col + 1]);   
                        }
                    }
                }
            }

        }

        private static void OutputMatrix(int[,] matrix, int[] characteristics) // Метод вывода матрицы и её характеристики в консоль
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + "\t");
                }
                Console.Write('\n');
            }
            Console.Write("\n\n");
            for (int iter = 0; iter < characteristics.Length; iter++)
            {
                Console.Write(characteristics[iter] + "\t");
            }
            Console.Write("\n\n");
        }

        private static void Swap(ref int a, ref int b) // Метод, который меняет местами значения двух переменных
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}