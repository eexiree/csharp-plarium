using System;
using System.Collections.Generic;
using System.Text;

namespace Task_A.Classes
{
    public static class StringExtension
    {

        #region Select Special Word Method
        public static void SelectSpecialWord(this string text, string searchedWord) // Метод для выделения заданого слова
        {
            foreach (var index in NextWordIndex(text, searchedWord))    // Проходимся по каждому индексу, которые возвращает метод. Проход в цикле foreach возможен, благодаря типу IEnumerable, возвращаемого методом NextWordIndex
            {
                if (index == -1)    // Если индекс начала слова -1, то очищаем консоль и выходим из цикла 
                {
                    Console.Clear();
                    break;
                }
                Console.Clear();
                Console.WriteLine($"\nSearched word: {searchedWord}\n");
                for (int i = 0; i < text.Length; i++)   // Выводим на экран строку используя цикл for
                {
                    if (i - index >= 0 && i - index <= searchedWord.Length - 1) // Если, при прохождении цикла попадаем в данный диапазон, меняем цвет foreground консоли
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(text[i]); // Выводим символ строки
                    Console.ResetColor();   // Сбрасываем свойства консоли на дефолтные
                }
                Console.ReadKey();  // Ждем, пока юзер нажмёт какую-либо клавишу, чтобы отобразить следующее слово
            }
        }

        private static IEnumerable<int> NextWordIndex(string inputText, string searchWord) // Метод который возвращает первые индексы слов в строке
        {
            int lastIndex = -1; // Переменная, которая запоминает, от какого индекса в строке начинать поиск
            do { yield return lastIndex = inputText.IndexOf(searchWord, lastIndex + 1); }   // Возвращаем найденный индекс
            while (lastIndex >= 0); // Производим поиск, пока IndexOf не вернет -1
        }

        #endregion

        #region Remove Verbs Method

        public static void RemoveVerbs(this string inputText) // Метод, который принимает строку на русском языке и выводит на консоль строку без глаголов
        {
            string[] verbsEndings = { "ешь", "ет", "ем", "ете", "ут", "ют", "ишь", "ит", "им", "ите", "aт", "ят", "ть" }, // Массив в котором храним возможные окончания глаголов в русском языке
                     words = inputText.ToLower().Split(new char[] { '.', ',', ':', ';', ' ' }); // Массив строк в котором храним отдельные слова
            StringBuilder outputText = new StringBuilder();

            bool IsVerb = false;    // Флаг, который говорит - является ли (возможно) слово глаголом

            foreach(var word in words)  // Проходимся по каждому слову в массиве слов
            {
                IsVerb = false; //Обнуляем флаг
                foreach(var ending in verbsEndings) // Проходимся по каждому окончанию в массиве окончаний главголов
                {
                    if (word.EndsWith(ending))  // Проверяем, совпадает ли окончание слова с одним из окончаний из массива окончаний. Если да - указываем, что слово является глаголом, и выходим из цикла
                    {
                        IsVerb = true;
                        break;
                    }
                }
                if (!IsVerb) outputText.Append(word + " "); // Если слово не является глаголом - добавляем его в результирующую строку
            }
            Console.WriteLine("\n\nСтрока без глаголов:\t" + outputText); // Выводим результирующую строку
        }

        #endregion

        #region Find Longest Substring

        public static void Decompose(this string input) // Метод который находит найбольшую общую последовательность в строке и выводит "разбитое" слово на: префикс, корень и окончание
        {
            string[] words = input.ToUpper().Split(new char[] { '.', ',', ':', ';', ' ' }); // "Разбиваем" входящую строку на массив слов
            List<int> subStringsIndexes = null; // Список для хранения индексов подстрок, для отображения последних на консоли, как в задании 1

            (bool isFirstWordAdded, List<string> coll) decays = (false, new List<string>());    // Инициализируем кортеж, который состоит из флага (добавленно ли первое слово в список для отображения составных частей), и список строк в которых будем хранить "разбитое" слово, 
                                                                                                // т.е. префикс слова, корень и окончание
            string subString = string.Empty;                                                    // Переменная текущей подстроки
            for (int lp_1 = 0; lp_1 < words.Length; lp_1++)                                     // Проходимся по всем словам массива слов, таким образом, чтобы сравнить каждые пары слов на предмет наличия одинаковых подстрок
            {
                for(int lp_2 = lp_1 + 1; lp_2 < words.Length; lp_2++)
                {
                    var (wordA, wordB, lcs) = LCS(words[lp_1], words[lp_2]);                    // Получаем данные о "разбитых" словах из метода, а также найбольшую общую подстроку

                    if (wordA.rLen >= 3)    // Проверяем, является ли основание общей подстроки длинее чем три символа
                    {
                        subString = lcs;    // Запоминаем общую подстроку, для дальнейшего отображения на консоли
                        subStringsIndexes = GetSubstringIndexes(input, subString);  // Получаем список индексов подстрок в строке
                        if (!decays.isFirstWordAdded)   // Добавляем слова в список слов для отображения их составных частей
                        {
                            decays.coll.Add(GetDecayOfWord(words[lp_1], wordA));
                            decays.isFirstWordAdded = true;
                        }
                        decays.coll.Add(GetDecayOfWord(words[lp_2], wordB));
                    }
                }

                #region Output & Reset

                // Выводим на консоль строку, общие подстроки и составные части тех слов, в которых эти подстроки находятся

                Console.Write($"\nLongest common string: {subString}\n");
                OutputSelectedWords(input, subStringsIndexes, subString.Length);
                Console.WriteLine("\n\n" + new string('-', 20));

                foreach(var decay in decays.coll)
                {
                    Console.WriteLine(decay);
                }

                // Обнуляем значение переменных для дальнейшего их использования

                decays.coll.Clear();
                decays.isFirstWordAdded = false;

                subStringsIndexes.Clear();
                subString = string.Empty;

                Console.ReadKey();
                Console.Clear();

                #endregion
            }
        }

        private static string GetDecayOfWord(string word, WordData decayData) // Метод, который составляет строку из составных частей слова
        {
            return $"\nPrefix: {word.Substring(0, decayData.pLen)}" +
                   $"\nRoot: {word.Substring(decayData.pLen, decayData.rLen)}" +
                   $"\nEnding: {word.Substring(decayData.pLen + decayData.rLen, decayData.eLen)}";
        }

        private static void OutputSelectedWords(string input, List<int> wordsIndexes, int subStringLength) // Метод, который выводит на консоль строку с выделенными подстроками
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (wordsIndexes.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    for (int j = 0; j < subStringLength; j++, i++)
                    {
                        Console.Write(input[i]);
                    }
                    Console.ResetColor();
                    if (i == input.Length) break;
                }
                Console.Write(input[i]);
            }
        }

        private static List<int> GetSubstringIndexes(string input, string subString) // Метод, который возвращает список индексов общих подстрок в строке
        {
            List<int> indexes = new List<int>();
            int lastIndex = -1;
            do 
            {  
                 indexes.Add(lastIndex = input.IndexOf(subString, lastIndex + 1));
            }
            while (lastIndex >= 0);
            return indexes;
        }

        private static ValueTuple<WordData, WordData, string> LCS(string a, string b)   // Метод для нахождения общей найбольшей подстроки 
        {                                                                              
            int[,] collisionMatrix = new int[a.Length, b.Length];   // Двумернный массив для хранения совпадающих символов
            int mathesValue = 0, mathesIndex = 0;                   // Вспомогательный переменные для хранение найбольшей длинны общей подстроки и индекс начала подстроки

            for(int i = 0; i < a.Length; i++)   // Проходимся по каждому из элементов двум строк, в поисках одинаковых символов
            {
                for(int j = 0; j < b.Length; j++)
                {
                    if (a[i] == b[j])   // Если произошло совпадение - вычисляем значение элемента двумерного массива коллизий символов
                    {
                        collisionMatrix[i, j] = (i == 0 || j == 0) ? 1 : collisionMatrix[i - 1, j - 1] + 1;
                        if (collisionMatrix[i, j] > mathesValue)
                        {
                            mathesValue = collisionMatrix[i, j];
                            mathesIndex = i;
                        }
                    }
                }
            }
            string lcs = a.Substring(mathesIndex + 1 - mathesValue, mathesValue);   // Получаем общую найбольшую подстроку из исходных двух
            return (new WordData(                               // Возвращаем структуры, в которых хранятся длинны префикса, основания и окончания входных слов, а также общую найбольшую подстроку
                        a.IndexOf(lcs),
                        lcs.Length,
                        a.Length - a.IndexOf(lcs) - lcs.Length), 
                    new WordData(
                        b.IndexOf(lcs),
                        lcs.Length,
                        b.Length - b.IndexOf(lcs) - lcs.Length),
                    lcs);
        }

        public struct WordData  // Структура для хранения длин префикса, основания и окончания слова
        {
            public readonly int pLen;   //  prefix length
            public readonly int rLen;   //  root length
            public readonly int eLen;   //  ending length

            public WordData(int prefix, int root, int ending)
            {
                pLen = prefix;
                rLen = root;
                eLen = ending;
            }
        }

        #endregion

    }
}
