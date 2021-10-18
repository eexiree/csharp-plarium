// Курс по основам C# от компании Plarium
// Боярский Алексей

using Task_A.Classes;
using System;

namespace Task_A
{
    class EntryPoint
    {
        static void Main()
        {
            Console.WriteLine(new string('-', 30) + "\n\nEnter the date (dd.mm.yyyy)\n");
            Date date = new Date(Console.ReadLine());
            Console.WriteLine(date);
            Console.WriteLine(new string('-', 30) + "\n\nEnter the date (dd.mm.yyyy) to find difference between dates\n");
            Console.WriteLine($"\nDays between is:\t{date.DaysBetween(new Date(Console.ReadLine()))}");
        }
    }
}
