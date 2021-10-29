using System;

namespace WatchShop
{
    class Program
    {
        static void Main()
        {
            Shop Gucci = new Shop("Gucci", 2, 50000);
            Shop Balenciaga = new Shop("Balenciaga", 1, 20000);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nАссортименты магазинов перед покупкой/продажей\n\n");
            Console.ResetColor();
            Console.WriteLine(Gucci);
            Console.WriteLine(Balenciaga);

            ExchangeEventArgs args = new ExchangeEventArgs()
            {
                Amount = 3,
                Watch = Balenciaga.Assortment[0],
                Seller = Balenciaga,
                Buyer = Gucci
            };

            try
            {
                Agent.Exchange(args, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nАссортименты магазинов после покупки/продажи\n\n");
            Console.ResetColor();
            Console.WriteLine(Gucci);
            Console.WriteLine(Balenciaga);

            Console.ReadKey();
            Console.Clear();

            string input;
            decimal cost;
            int type;

            Shop shop = new Shop("LV", 10, 40000);
            Console.WriteLine(shop);

            Console.ForegroundColor = ConsoleColor.Green;

            #region Вывести марки часов, изготовленных в заданной стране
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nМарки часов, изготовленные в заданной стране.\nВведите страну:\n");
            while (shop.Assortment.IsCountryAvailable(input = Console.ReadLine()) == false);
            Console.ResetColor();
            foreach (var item in shop.Assortment.BrandByCountry(input))
            {
                Console.WriteLine("\n" + item);
            }
            #endregion

            #region Вывести информацию о механических часах, цена на которые не превышает заданную
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nМеханические часы, цена которых не превышает заданную.\nВведите цену:\n");
            while (decimal.TryParse(Console.ReadLine(), out cost) == false);
            Console.ResetColor();
            foreach (var item in shop.Assortment.MechWatchesByCostRange(cost))
            {
                Console.WriteLine("\n" + item);
            }
            #endregion

            #region Вывести марки заданного типа часов.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nМарки часов заданого типа.\n1.Механические\n2.Кварцевые\n");
            while ((int.TryParse(Console.ReadLine(), out type) && (type == 1 || type == 2)) == false);
            Console.ResetColor();
            foreach (var item in shop.Assortment.BrandByType((WatchType)(type - 1)))
            {
                Console.WriteLine("\n" + item);
            }
            #endregion

            #region Вывести производителей, общая сумма часов которых в магазине не превышает заданную
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nПроизводители, общая сумма часов которых в магазине не превышает заданную.\nВведите цену:\n");
            while (decimal.TryParse(Console.ReadLine(), out cost) == false);
            Console.ResetColor();
            foreach (var item in shop.Assortment.ProducersByTotalCost(cost))
            {
                Console.WriteLine("\n" + item);
            }
            #endregion

            #region Сортировка ассортимента магазина по одному из условий
            type = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nСортировка ассортимента магазина по одному из условий\n1.По цене\n2.По типу\n3.По количеству\n4.По стране\n5.По производителю\n6.По бренду\n");
            while ((int.TryParse(Console.ReadLine(), out type) && (type >= 1 && type <= 6)) == false) ;
            Console.ResetColor();
            switch (type)
            {
                case 1: 
                    shop.AssortmentByCost();
                    break;
                case 2:
                    shop.AssortmentByType();
                    break;
                case 3:
                    shop.AssortmentByAmount();
                    break;
                case 4:
                    shop.AssortmentByCountry();
                    break;
                case 5:
                    shop.AssortmentByProducer();
                    break;
                case 6:
                    shop.AssortmentByBrand();
                    break;
            }
            Console.WriteLine(shop);
            #endregion
        }
    }
}
