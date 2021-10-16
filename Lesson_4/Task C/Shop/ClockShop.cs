using System.Collections.Generic;
using static System.Console;

namespace Clock_Shop
{
    public static class ClockShop
    {
        private static Clock[] _clocks = // Массива типа часов, в котором хранится информация о часах в магазине
        {
            new Clock("Kallista", ClockType.Quartz, 11000000, 1, new Producer("Vacheron Constantin", "Швейцария")),
            new Clock("Watch", ClockType.Mechanical, 50000, 7, new Producer("Breguet", "Швейцария")),
            new Clock("1-2099D", ClockType.Quartz, (decimal)120.5, 15, new Producer("JACQUES LEMANS", "Австрия")),
            new Clock("Baby-G", ClockType.Mechanical, (decimal)135.7, 17, new Producer("Casio", "Япония")),
            new Clock("Cwba-110", ClockType.Quartz, 150, 12,  new Producer("Casio", "Япония")),
            new Clock("ES3202", ClockType.Quartz, (decimal)92.3, 6, new Producer("Fossil", "США")),
            new Clock("Wild", ClockType.Mechanical, 45, 15, new Producer("Curren", "КНР")),
            new Clock("Jaragar Elite", ClockType.Mechanical, 110, 18, new Producer("Jaragar", "КНР"))
        };

        public static List<string> AvailableCountries(bool output = false)  // Метод, который предоставляет список стран для выбора (По условиям задачи есть пункт, в котором необходимо вывести часы, изготовленные в определенной стране)
        {
            List<string> availableCountries = new List<string>();
            string country;
            if(output) WriteLine("Доступные страны:");
            foreach(var clock in _clocks)
            {
                country = clock.Details.Country;
                if (availableCountries.Contains(country))   // Если страна уже есть в списке, не добавляем её заново
                    continue;
                else
                {
                    if (output)
                    {
                        WriteLine(country);
                    }
                        
                    availableCountries.Add(country);
                }
            }
            return availableCountries;
        }

        public static void BrandByClockType(ClockType type) // Метод который выводит бренды часов по их типу
        {
            foreach(var clock in _clocks)
            {
                if(clock.Type == type)
                {
                    WriteLine($"{clock.Brand}");
                }
            }
        }

        public static void MechanicalClocksInfo(decimal cost)   // Метод который выводит информацию о механических часах
        {
            foreach(var clock in _clocks)
            {
                if(clock.Type == ClockType.Mechanical && clock.Cost < cost)
                {
                    WriteLine(clock);
                }
            }
        }

        public static void BrandByCountry(string country)   // Метод который выводит бренды часов по их стране изготовления 
        {
            foreach(var clock in _clocks)
            {
                if(clock.Details.Country == country)
                {
                    WriteLine(clock.Brand);
                }
            }
        }

        public static void ProducersByTotalCost(decimal totalCost)  // Метод который выводит информацию о производителях, по суммарной стоймости часов в магазине
        {
            foreach(var clock in _clocks)
            {
                if (clock.Amount * clock.Cost < totalCost)
                    WriteLine(clock.Details.Name);
            }
        }
    
    }
}
