// Курс по основам C# от компании Plarium
// Боярский Алексей 

//                          ---- Задание по коллекциям ----
//  Магазин часов.
//  В сущностях (типах) хранится информация о часах, продающихся в магазине.
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
using WatchShop.WatchData;

namespace WatchShop
{
    class EntryPoint
    {
        static void Main()
        {
            string input;
            decimal cost;
            int type;

            WatchCollection coll = new WatchCollection();
            coll.FillIn(5);
            Console.WriteLine(coll);

            #region Вывести марки часов, изготовленных в заданной стране
            Console.WriteLine("\n\nМарки часов, изготовленные в заданной стране.\nВведите страну:\n");
            while (coll.IsCountryAvailable(input = Console.ReadLine()) == false);
            foreach (var item in coll.BrandByCountry(input))
            {
                Console.WriteLine("\n" + item);
            }
            #endregion

            #region Вывести информацию о механических часах, цена на которые не превышает заданную
            Console.WriteLine("\n\nМеханические часы, цена которых не превышает заданную.\nВведите цену:\n");
            while (decimal.TryParse(Console.ReadLine(), out cost) == false);
            foreach (var item in coll.MechWatchesByCostRange(cost))
            {
                Console.WriteLine("\n" + item);
            }
            #endregion

            #region Вывести марки заданного типа часов.
            Console.WriteLine("\n\nМарки часов заданого типа.\n1.Механические\n2.Кварцевые\n");
            while ((int.TryParse(Console.ReadLine(), out type) && (type == 1 || type == 2)) == false);
            foreach (var item in coll.BrandByType((WatchType)(type - 1)))
            {
                Console.WriteLine("\n" + item);
            }
            #endregion

            #region Вывести производителей, общая сумма часов которых в магазине не превышает заданную
            Console.WriteLine("\n\nПроизводители, общая сумма часов которых в магазине не превышает заданную.\nВведите цену:\n");
            while (decimal.TryParse(Console.ReadLine(), out cost) == false);
            foreach (var item in coll.ProducersByTotalCost(cost))
            {
                Console.WriteLine("\n" + item);
            }
            #endregion
        }
    }
}
