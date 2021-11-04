using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using WatchShop.DB;

namespace WatchShop.UI
{
    public static class UserInterface
    {
        #region Fields

        [DllImport("Kernel32")] public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        private static List<Shop> capturedShops = new List<Shop>();
        private static Shop capturedShop;

        private static Dictionary<string, Action> MainPage = new Dictionary<string, Action>
        {
            ["Data Base"]           = () => new Page(DataBasePage),
            ["Captured Shops"]      = CapturedShops
        };

        private static Dictionary<string, Action> DataBasePage = new Dictionary<string, Action>
        {
            ["Create shop"]         = CreateShop,
            ["Capture shop"]        = CaptureShop,
            ["Capture shops"]       = CaptureShops,
            ["Reset"]               = Reset,
            ["Show"]                = Show,
            ["Show Logs"]           = ShowLogs
        };

        private static Dictionary<string, Action> CapturedShopOperations = new Dictionary<string, Action>
        {
            ["Show Assortment"]     = ShowAssortment,
            ["Add watch"]           = AddWatch,
            ["Find specific items"] = () => new Page(FindSpecificItem),
            ["Ordering"]            = () => new Page(Ordering)
        };

        private static Dictionary<string, Action> FindSpecificItem = new Dictionary<string, Action>
        {
            ["Brand by Country"]    = BrandByCountry,
            ["Brand by Type"]       = BrandByType,
            ["Mechanical watches by cost range"] = MechWatchesByCostRange,
            ["Producers by total assortment cost"] = ProducersByTotalCost
        };

        private static Dictionary<string, Action> Ordering = new Dictionary<string, Action>
        {
            ["Order by brand"]          = AssortmentByBrand,
            ["Order by type"]           = AssortmentByType,
            ["Order by cost"]           = AssortmentByCost,
            ["Order by amount"]         = AssortmentByAmount,
            ["Order by producer name"]  = AssortmentByProducer,
            ["Order by country"]        = AssortmentByCountry
        };

        #endregion

        public static void Init()
        {
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

            var backups = DataBase.RestoreBackups();
            if (backups is not null)
                capturedShops.AddRange(backups);

            Page mainPage = new Page(MainPage);
        }

        #region DataBase

        private static void CreateShop()
        {
            string shopName = default;
            decimal shopMoney = default;
            Shop shop = null;
            Console.Clear();
            try
            {
                Console.WriteLine("Creating a new shop:\n");
                Console.Write("Name:\t");
                shopName = Console.ReadLine() ?? "Unknown";
                Console.Write("Money:\t");
                shopMoney = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Fill randomly?\t");
                shop = new Shop(shopName, shopMoney);
                if (Convert.ToBoolean(Console.ReadLine()))
                {
                    Console.Write("Enter the number of watches to fill in ");
                    shop.Assortment.FillIn(Convert.ToInt32(Console.ReadLine()));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + "Press any key to go back");
                Console.ReadKey();
            }
            DataBase.Safe(shop);
            Console.WriteLine("The shop was created successfuly");
            Console.ReadKey();
        }

        private static void CaptureShop()
        {
            int currentLine = 0;
            ConsoleKeyInfo keyInfo = default;
            var shopsInDB = DataBase.GetShopsNames();
            while (keyInfo.Key != ConsoleKey.Escape)
            {
                if (shopsInDB.Count is 0)
                    return;
                Console.Clear();
                for (int i = 0; i < shopsInDB.Count; i++)
                {
                    if (currentLine == i) Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(shopsInDB[i]);
                    Console.ResetColor();
                }
                Console.WriteLine("\nEnter escape to go back\n");
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentLine--;
                        if (currentLine < 0)
                            currentLine = shopsInDB.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentLine++;
                        if (currentLine >= shopsInDB.Count)
                            currentLine = 0;
                        break;
                    case ConsoleKey.Enter:
                        if (shopsInDB.Count > 0)
                        {
                            string shopName = shopsInDB[currentLine];
                            capturedShops.Add(DataBase.ReadShopData(shopName));
                            shopsInDB.Remove(shopName);
                        }
                        break;
                }
            }
        }
        
        private static void CaptureShops()
        {
            var shopsInDB = DataBase.ReadShopsData();
            if(shopsInDB is not null)
                capturedShops.AddRange(shopsInDB);
        }

        private static void Reset()
        {
            DataBase.ResetDataBase();
        }

        private static void Show()
        {
            Console.Clear();
            Console.WriteLine(DataBase.GetDBData);
            Console.WriteLine("\nEnter any key to go back\n");
            Console.ReadKey();
        }

        private static void ShowLogs()
        {
            int currentLine = 0;
            ConsoleKeyInfo keyInfo = default;
            var logs = DataBase.GetLogFiles();
            while (keyInfo.Key != ConsoleKey.Escape)
            {
                Console.Clear();
                for (int i = 0; i < logs.Length; i++)
                {
                    if (currentLine == i) Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(logs[i].Name);
                    Console.ResetColor();
                }
                Console.WriteLine("\nEnter escape to go back\n");
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentLine--;
                        if (currentLine < 0)
                            currentLine = logs.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentLine++;
                        if (currentLine >= logs.Length)
                            currentLine = 0;
                        break;
                    case ConsoleKey.Enter:
                        if (logs.Length > 0)
                        {
                            ShowLog(logs[currentLine].FullName);
                        }
                        break;
                }
            }
        }

        private static void ShowLog(string path)
        {
            Console.Clear();
            Console.WriteLine(File.ReadAllText(path));
            Console.WriteLine("\nEnter any key to go back\n");
            Console.ReadKey();
        }

        #endregion

        #region Captured Shops

        private static void CapturedShops()
        {
            int currentLine = 0;
            ConsoleKeyInfo keyInfo = default;
            List<string> shops = null;
            if(capturedShops is not null)
                shops = (from s in capturedShops select s.Name).ToList();
            if (shops.Count > 0)
                shops.Add("Save all shops");
            else return;
                

            while (keyInfo.Key != ConsoleKey.Escape)
            {
                Console.Clear();
                
                for (int i = 0; i < shops.Count; i++)
                {
                    if (currentLine == i) Console.ForegroundColor = ConsoleColor.Green;
                    if (i == shops.Count - 1)
                        Console.Write(Environment.NewLine);
                    Console.WriteLine(shops[i]);
                    Console.ResetColor();
                }
                Console.WriteLine("\nTo save the shop to DB choose it and PRESS F5");
                Console.WriteLine("\nEnter any key to go back\n");
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentLine--;
                        if (currentLine < 0)
                            currentLine = shops.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentLine++;
                        if (currentLine >= shops.Count)
                            currentLine = 0;
                        break;
                    case ConsoleKey.F5:
                        if(currentLine < shops.Count - 1)
                        {
                            DataBase.Safe(capturedShops[currentLine]);
                            capturedShops.RemoveAt(currentLine);
                            shops.RemoveAt(currentLine);
                        }
                        break;
                    case ConsoleKey.Enter:
                        if(currentLine == shops.Count - 1)
                        {
                            DataBase.Safe(capturedShops);
                            capturedShops.Clear();
                            shops.Clear();
                            return;
                        }
                        else if (capturedShops.Count > 1 && currentLine < capturedShops.Count)
                        {
                            capturedShop = capturedShops[currentLine];
                            new Page(CapturedShopOperations);
                        }
                        break;
                }
            }
        }

        private static void ShowAssortment()
        {
            Console.Clear();
            Console.WriteLine(capturedShop);
            Console.WriteLine("\nPress any key to go back\n");
            Console.ReadKey();
        }

        private static void AddWatch()
        {
            Watch watch = new Watch();
            Console.Clear();
            ConsoleKeyInfo keyInfo = default;
            int typeLine = 0;
            try
            {
                Console.Write("Brand:\t");
                watch.Brand = Console.ReadLine() ?? "China Copy";
                var cursorPos = Console.GetCursorPosition();
                while(keyInfo.Key != ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);
                    Console.Write("Type:\t");
                    if(typeLine == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write("Quartz\t");
                    Console.ResetColor();
                    if (typeLine == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write("Mechanical");
                    Console.ResetColor();
                    keyInfo = Console.ReadKey();
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (typeLine == 0)
                                typeLine = 1;
                            else typeLine = 0;
                            break;
                        case ConsoleKey.RightArrow:
                            if (typeLine == 1)
                                typeLine = 0;
                            else typeLine = 1;
                            break;
                        case ConsoleKey.Enter:
                            watch.Type = (WatchType)typeLine;
                            break;
                    }
                }
                Console.Write("\nCost:\t");
                watch.Cost = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Amount:\t");
                watch.Amount = Convert.ToInt32(Console.ReadLine());
                Console.Write("Producer:\t");
                watch.ProducerData.Name = Console.ReadLine() ?? "China Corp.";
                Console.Write("Country:\t");
                watch.ProducerData.Country = Console.ReadLine() ?? "China";
                capturedShop.Assortment.Add(watch);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }
        }

        #region Find specific item

        private static void BrandByCountry()
        {
            int currentLine = 0;
            ConsoleKeyInfo keyInfo = default;
            string country = default;
            var aCountries = capturedShop.Assortment.AvailableCountries.ToList();
            
            
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("Brand by Country. Choose the country:\n");
                for (int i = 0; i < aCountries.Count; i++)
                {
                    if (i == currentLine) Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(aCountries[i]);
                    Console.ResetColor();
                }
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentLine--;
                        if (currentLine < 0)
                            currentLine = aCountries.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentLine++;
                        if (currentLine >= aCountries.Count)
                            currentLine = 0;
                        break;
                    case ConsoleKey.Enter:
                        if (aCountries.Count > 0)
                        {
                            country = aCountries[currentLine];
                        }
                        else return;
                        break;
                }
            }
            foreach (var watch in capturedShop.Assortment.BrandByCountry(country))
            {
                Console.WriteLine("\n" + watch);
            }
            Console.WriteLine("\nEnter any key to go back\n");
            Console.ReadKey();
        }

        private static void BrandByType()
        {
            ConsoleKeyInfo keyInfo = default;
            int type = 0;
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("Brand by type. Choose the type:\n");
                if (type == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write("Quartz\t");
                Console.ResetColor();
                if (type == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write("Mechanical");
                Console.ResetColor();
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (type == 0)
                            type = 1;
                        else type = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        if (type == 1)
                            type = 0;
                        else type = 1;
                        break;
                }
            }
            Console.WriteLine();
            foreach (var brand in capturedShop.Assortment.BrandByType((WatchType)type))
            {
                Console.WriteLine("\n" + brand);
            }
            Console.WriteLine("\nEnter any key to go back\n");
            Console.ReadKey();
        }

        private static void MechWatchesByCostRange()
        {
            decimal cost;
            Console.Clear();
            Console.WriteLine("Mechanical watches by cost range. Enter the cost:\n");
            while (decimal.TryParse(Console.ReadLine(), out cost) == false);
            foreach (var item in capturedShop.Assortment.MechWatchesByCostRange(cost))
            {
                Console.WriteLine("\n" + item);
            }
            Console.WriteLine("\nEnter any key to go back\n");
            Console.ReadKey();
        }

        private static void ProducersByTotalCost()
        {
            decimal cost;
            Console.Clear();
            Console.WriteLine("Producers, which watches cost is less than... Enter the cost:\n");
            while (decimal.TryParse(Console.ReadLine(), out cost) == false);
            foreach (var item in capturedShop.Assortment.ProducersByTotalCost(cost))
            {
                Console.WriteLine("\n" + item);
            }
            Console.WriteLine("\nEnter any key to go back\n");
            Console.ReadKey();
        }

        #endregion

        #region Ordering

        private static void AssortmentByBrand()
        {
            capturedShop.AssortmentByBrand();
        }

        private static void AssortmentByType()
        {
            capturedShop.AssortmentByType();
        }

        private static void AssortmentByCost()
        {
            capturedShop.AssortmentByCost();
        }

        private static void AssortmentByAmount()
        {
            capturedShop.AssortmentByAmount();
        }

        private static void AssortmentByProducer()
        {
            capturedShop.AssortmentByProducer();
        }
            
        private static void AssortmentByCountry()
        {
            capturedShop.AssortmentByCountry();
        }

        #endregion

        #endregion

        #region Backup Maker

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            DataBase.BackupShops(capturedShops);
            return true;
        }

        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        #endregion
    }
}