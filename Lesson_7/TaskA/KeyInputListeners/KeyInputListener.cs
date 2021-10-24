using System;

namespace KeyInputListeners
{
    public class KeyEventArgs : EventArgs   // Класс, который хранит введеный символ
    {
        public char inputChar;
    }

    public class KeyEventDisplay            // Класс, который отвечает за вывод сообщения о том, что был введен символ
    {
        public static void Display(object sender, KeyEventArgs arg)
        {
            Console.SetCursorPosition(0, KeyEventCounter.Counter + 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\nПолучено сообщение о нажатии клавиши: {arg.inputChar}");
            Console.ResetColor();
        }
    }

    public static class KeyEventCounter     // Класс который отвечает за подсчитывание введеных символов
    {
        public static int Counter
        {
            get;
            private set;
        }

        public static void IncrementCounter(object sender, KeyEventArgs arg)
        {
            Counter++;
        }
    }
}
