﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchShop.UI
{
    public class Page
    {
        public Page(Dictionary<string, Action> actions)
        {
            Init(actions);
        }

        private void Init(Dictionary<string, Action> actions)
        {
            int currentLine = 0;
            ConsoleKeyInfo keyInfo = default;
            var keys = actions.Keys.ToArray();
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < keys.Length; i++)
                {
                    if (currentLine == i) Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(keys[i]);
                    Console.ResetColor();
                }
                Console.WriteLine("\nEnter escape to go back\n");
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentLine--;
                        if (currentLine < 0)
                            currentLine = keys.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentLine++;
                        if (currentLine >= keys.Length)
                            currentLine = 0;
                        break;
                    case ConsoleKey.Enter:
                        if(actions.Count > 0)
                            actions[keys[currentLine]].Invoke();
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        return;
                }
            }
        }
    }
}
