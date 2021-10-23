// Курс по основам C# от компании Plarium
// Боярский Алексей 

//  Магазин часов.В сущностях (типах) хранится информация о часах, продающихся в магазине.
//  Для часов необходимо хранить:
//      — марку;
//      — тип(кварцевые, механические);
//      — цену;
//      — количество;
//      — реквизиты производителя.
//  Для производителей необходимо хранить:
//      — название;
//      — страна.
//  Вывести марки заданного типа часов.
//  Вывести информацию о механических часах, цена на которые не превышает заданную.
//  Вывести марки часов, изготовленных в заданной стране.
//  Вывести производителей, общая сумма часов которых в магазине не превышает заданную.


using System;
using static System.Console;

namespace Clock_Shop
{
    class EntryPoint
    {
        static void Main()
        {            
            #region Вывести марки заданного типа часов.
            WriteLine("Вывести информацию о механических часах, цена на которые не превышает заданную\n\nВведите тип часов\n1:Quartz\t2:Mechanical\n");
            byte input;
            while (!(byte.TryParse(ReadLine(), out input) && (input == 1 || input == 2)));
            ClockShop.BrandByClockType((ClockType)input - 1);
            Console.ReadKey();
            Clear();
            #endregion

            #region Вывести информацию о механических часах, цена на которые не превышает заданную
            WriteLine("Вывести информацию о механических часах, цена на которые не превышает заданную\n\nВведите цену:\n");
            decimal cost;
            while (!decimal.TryParse(ReadLine(), out cost));
            ClockShop.MechanicalClocksInfo(cost);
            Console.ReadKey();
            Clear();
            #endregion

            #region Вывести марки часов, изготовленных в заданной стране
            WriteLine("Вывести марки часов, изготовленных в заданной стране\n\nВведите страну\n");
            ClockShop.AvailableCountries(true);
            string country;
            while (!ClockShop.AvailableCountries().Contains(country = ReadLine()));
            ClockShop.BrandByCountry(country);
            Console.ReadKey();
            Clear();
            #endregion

            #region Вывести производителей, общая сумма часов которых в магазине не превышает заданную
            WriteLine("\nВывести производителей, общая сумма часов которых в магазине не превышает заданную\nВведите общую цену часов в магазине:");
            decimal totalCost;
            while (!decimal.TryParse(ReadLine(), out totalCost) == false);
            ClockShop.ProducersByTotalCost(totalCost);
            Console.ReadKey();
            Clear();
            #endregion

            Console.ReadKey();
        }
    }
}
