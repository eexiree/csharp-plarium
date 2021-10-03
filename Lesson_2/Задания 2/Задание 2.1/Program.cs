// Курс по основам C# от компании Plarium
// Боярский Алексей     
// 18 вариант по списку -> задания 8 варианта

// 2.1 - 8е задание
// Напечатать в возрастающем порядке все трехзначные числа,
// в десятичной записи которых нет одинаковых цифр. Операции
// деления, целочисленного деления и определения остатка не использовать.

using System;

namespace Task_2_1 {

    class EntryPoint {

        public static void Main() {
            for (int i = 100; i < 1000; i++) {
                if (ConsistUniqueNumerals(i)) {
                    Console.Write(i + " / ");
                }
            }
            Console.ReadKey();
        }

        private static bool ConsistUniqueNumerals(int value) {
            string strRep = value.ToString();
            return strRep[0] != strRep[1] &&
                   strRep[0] != strRep[2] &&
                   strRep[1] != strRep[2] ? true : false;
        }
    }
}