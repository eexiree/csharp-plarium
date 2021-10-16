// Курс по основам C# от компании Plarium
// Боярский Алексей

// Задание А
// 1: Программа принимает строку. 
//    По нажатию произвольной клавиши поочередно выделяет в тексте заданное слово (заданное слово вводится с клавиатуры).
// 
// 2: Ищет в ней глаголы и возвращает в консоль строку без глаголов.
//    Для выполнения задания создать массив строк и проинициализировать
//    его несколькими окончаниями, которые есть у глаголов, например,
//    “ать”, “ять” и т.д. Слово из входной строки соответствует глаголу,
//    если оно содержит одно из этих окончаний.
//
// 3: Найти во входной строке слова с одинаковым основанием
//    (совпадающие части двух и более слов, 3 буквы и более) и разбить эти слова на 3 части:
//      – префикс, то что стоит до основания слева,
//      – основа, то что совпадает с частью другого слова,
//      – окончание.
//    Обратите внимание, что некоторые из этих 1,3 частей могут отсутствовать.




using System;
using Task_A.Classes;

namespace Task_A
{
    class EntryPoint
    {
        static void Main()
        {
            string text;

            #region Task 1
            Console.WriteLine("Задание 1\n\nВведите текст:\n\n");
            text = Console.ReadLine();
            Console.WriteLine("\n\nВведите слово для поиска:\n\n");
            string searchedWord = Console.ReadLine();
            text.SelectSpecialWord(searchedWord);
            #endregion

            #region Task 2
            Console.Clear();
            Console.WriteLine("Задание 2\n\nВведите текст (на русском языке):\n\n");
            text = Console.ReadLine();
            text.RemoveVerbs();
            Console.ReadKey();
            #endregion

            #region Task 3
            Console.Clear();
            Console.WriteLine("Задание 3\n\nВведите текст:\n\n");
            text = Console.ReadLine();
            text.Decompose();
            #endregion
        }
    }
}
