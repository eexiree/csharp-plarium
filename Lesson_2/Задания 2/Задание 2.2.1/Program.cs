// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 2.2.1 - 8е задание
// Дано натуральное число. Определить, все ли цифры в нем одинаковы.

using System;

namespace Task_2_2_1 {

    public class EntryPoint {

        public static void Main() {
            int inputValue;
            Console.WriteLine("Enter a value:");
            if (!Int32.TryParse(Console.ReadLine(), out inputValue)) {
                Console.WriteLine("Entered value is not integer\nExiting...");
                Console.ReadKey();
                return;
            }
            if (ConsistIdenticalNumerals(inputValue)) {
                Console.WriteLine("Integer consist of identical numerals");
            }
            else {
                Console.WriteLine("Integer consist of non identical numerals");
            }
            Console.ReadKey();
        }

        private static bool ConsistIdenticalNumerals(int value) {
            string strRep = value.ToString();
            for (int i = 0; i < strRep.Length - 1; i++) {
                if (strRep[i] != strRep[i + 1])
                    return false;
            }
            return true;
        }

    }

}