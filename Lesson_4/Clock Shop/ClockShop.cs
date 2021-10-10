using System.Collections.Generic;
using static System.Console;

namespace Clock_Shop
{
    public static class ClockShop
    {
        private static Clock[] _clocks = 
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

        public static List<string> AvailableCountries(bool output = false)
        {
            List<string> availableCountries = new List<string>();
            string country;
            if(output) WriteLine("Доступные страны:");
            foreach(var clock in _clocks)
            {
                country = clock.Details.Country;
                if (availableCountries.Contains(country))
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

        public static void BrandByClockType(ClockType type)
        {
            foreach(var clock in _clocks)
            {
                if(clock.Type == type)
                {
                    WriteLine($"{clock.Brand}");
                }
            }
        }

        public static void MechanicalClocksInfo(decimal cost)
        {
            foreach(var clock in _clocks)
            {
                if(clock.Type == ClockType.Mechanical && clock.Cost < cost)
                {
                    WriteLine(clock);
                }
            }
        }

        public static void BrandByCountry(string country)
        {
            foreach(var clock in _clocks)
            {
                if(clock.Details.Country == country)
                {
                    WriteLine(clock.Brand);
                }
            }
        }

        public static void ProducersByTotalCost(decimal totalCost)
        {
            foreach(var clock in _clocks)
            {
                if (clock.Amount * clock.Cost < totalCost)
                    WriteLine(clock.Details.Name);
            }
        }
    
    }
}
