// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 2.2.2 - 8e задание
// Дано натуральное число.
// Определить, верно ли,
// что произведение его цифр меньше а,
// а само число делится на b?

using System;
using System.Collections.Generic;

namespace Task_2_2_2 {

    public class EntryPoint {

        public static void Main() {
            Console.WriteLine("Enter integer value, then a and b");
            uint inputValue, a, b, dotProduct = 1;
            if (!UInt32.TryParse(Console.ReadLine(), out inputValue) ||             // Проверка входных данных
                !UInt32.TryParse(Console.ReadLine(), out a) ||
                !UInt32.TryParse(Console.ReadLine(), out b)) 
            {
                Console.WriteLine("One of these entered values contain unsuported type\nExiting...");
                Console.ReadKey();
                return;
            }

            foreach (var num in DecomposeValue(inputValue)) { // Вычисляем произведение цифр, из которых состоит число
                dotProduct *= num;
            }

            if (dotProduct < a && inputValue % b == 0) {    // Выполняем проверки, заданные по условию
                Console.WriteLine($"Entered value dot product of each numeral: {dotProduct} is less than {a} \nand {inputValue} is divided entirely into {b}\nSuccessful examination");
            }
            else if (dotProduct >= a) {
                Console.WriteLine($"Entered value dot product of each numeral: {dotProduct} >= than {a}\nUnsuccessful examination");
            }
            else {
                Console.WriteLine($"Entered value {inputValue} is not divisible on {b}\nUnsuccessful examination");
            }
            Console.ReadKey();
        }

        private static byte[] DecomposeValue(uint value) {  // Метод разбиения числа на состовляющие его цифры
            List<byte> decomposedValue = new List<byte>();  // Возможно нельзя использовать список, но это самый короткий вариант,
            while (value > 0) {                             // который я придумал
                decomposedValue.Add((byte)(value % 10));
                value /= 10;
            }
            decomposedValue.Reverse();                      // Инверсируем список для получения правильного порядка цифр числа
            return decomposedValue.ToArray();               // Возвращаем массив цифр, полученого через метод копирования элементов из списка
        }

    }

}