// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 1.2 - 8e задание
// Дано натуральное число n, вычислить sqrt(3 + sqrt(6 + ... + sqrt(3 * n)))

using System;

namespace Task_1_2
{
    public class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("Enter n:");
            int inputValue;
            if(!Int32.TryParse(Console.ReadLine(), out inputValue)) // Проверка входных данных
            {
                Console.WriteLine("Entered value is not integer\nExiting...");
                return;
            }
            Console.WriteLine($"Answer with entered n({inputValue}) is: {ComputeValue(inputValue)}"); // Вывод результата
            Console.ReadKey();
        }

        public static double ComputeValue(int n) // Метод рассчета значения последовательности
        {
            double answer = 0;  // Переменная для суммирования промежуточных значений
            for(int i = 0; i < n; i++)
                answer = Math.Sqrt((double)(3 * (n - i)) + answer); // Рассчитываем значение
            return answer; // Возвращаем результат
        }
    }
}
