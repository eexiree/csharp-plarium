using System;
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
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < actions.Count; i++)
                {
                    if (currentLine == i) Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(actions.ElementAt(i).Key);
                    Console.ResetColor();
                }
                Console.WriteLine("\nEnter escape to go back\n");
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentLine--;
                        if (currentLine < 0)
                            currentLine = actions.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentLine++;
                        if (currentLine >= actions.Count)
                            currentLine = 0;
                        break;
                    case ConsoleKey.Enter:
                        if(currentLine >= 0 && currentLine < actions.Count)
                            actions?.ElementAt(currentLine).Value?.Invoke();
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        return;
                }
            }
        }
    }
}
