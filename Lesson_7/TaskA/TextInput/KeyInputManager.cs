using System;
using KeyInputListeners;

namespace TextInput
{
    public class KeyInputManager
    {
        public delegate void KeyHandler(object sender, KeyEventArgs arg);   // Определяем делегат для события 
        private event KeyHandler KeyPressed;                                // Событие, которое будет генирироваться после нажатии клавиши посредством вызова метода OnKeyPressed

        public void BeginInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Введите несколько символов. Для останова введите точку.");
            Console.ResetColor();

            KeyPressed += KeyEventDisplay.Display;          // Добавляем методы, которые необходимо вызвать при возникновении события
            KeyPressed += KeyEventCounter.IncrementCounter;

            KeyEventArgs arg = new KeyEventArgs();          // Создаем экзмепляр класса KeyEventArgs, чтобы не создавать новый экземпляр каждый раз, когда вводится символ (хранит значение нажатой клавиши)
            do
            {
                Console.SetCursorPosition(KeyEventCounter.Counter, 1);
                arg.inputChar = Console.ReadKey().KeyChar;
                OnKeyPressed(this, arg);    // Вызываем метод, который говорит, что была нажата клавиша (В качестве аргументов передаем текущий экзмепляр класса и переменную типа KeyEventArgs
            }
            while (arg.inputChar != '.');   // Выполняем считывание символов, пока не будет введена точка

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nБыло нажато {KeyEventCounter.Counter} клавиш");   // Выводим на консоль количество нажатых клавиш
            Console.ResetColor();
        }

        private void OnKeyPressed(object sender, KeyEventArgs arg)
        {
            KeyPressed?.Invoke(sender, arg);    // Генерируем событие, после того, как убедились что у него есть подписчики
        }
    }
}
