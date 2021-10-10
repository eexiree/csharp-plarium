﻿using System;
using static System.Console;

namespace Clock_Shop
{
    class EntryPoint
    {
        static void Main()
        {            

            #region Вывести марки заданного типа часов.
            WriteLine("Вывести информацию о механических часах, цена на которые не превышает заданную\n\nВведите тип часов\n1:Quartz\t2:Mechanical\n");
            while(byte.TryParse(ReadLine(), out byte input) && (input != 1 || input != 2))
            {
                ClockShop.BrandByClockType((ClockType)input - 1);
            }
            Clear();
            #endregion

            #region Вывести информацию о механических часах, цена на которые не превышает заданную
            WriteLine("Вывести информацию о механических часах, цена на которые не превышает заданную\n\nВведите цену:\n");
            while (decimal.TryParse(ReadLine(), out decimal cost))
            {
                ClockShop.MechanicalClocksInfo(cost);
            }
            Clear();
            #endregion

            #region Вывести марки часов, изготовленных в заданной стране
            WriteLine("Вывести марки часов, изготовленных в заданной стране\n\nВведите страну\n");
            ClockShop.AvailableCountries(true);
            string country;
            while(ClockShop.AvailableCountries().Contains(country = ReadLine()))
            {
                ClockShop.BrandByCountry(country);
            }
            Clear();
            #endregion

            #region Вывести производителей, общая сумма часов которых в магазине не превышает заданную
            WriteLine("\nВывести производителей, общая сумма часов которых в магазине не превышает заданную\nВведите общую цену часов в магазине:");
            while (decimal.TryParse(ReadLine(), out decimal totalCost))
            {
                ClockShop.ProducersByTotalCost(totalCost);
            }
            Clear();
            #endregion

            Console.ReadKey();
        }
    }
}