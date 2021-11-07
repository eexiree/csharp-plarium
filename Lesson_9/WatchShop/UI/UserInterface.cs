using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using WatchShop.DB;
using WatchShop.Args;

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
            ["Save shop"]           = SaveShop,
            ["Save shops"]          = SaveShops,
            ["Capture shop"]        = CaptureShop,
            ["Capture shops"]       = CaptureShops,
            ["Reset"]               = Reset,
            ["Show"]                = Show,
            ["Show Logs"]           = ShowLogs
        };

        private static Dictionary<string, Action> CapturedShopOperations = new Dictionary<string, Action>
        {
            ["Add watch"]           = AddWatch,
            ["Show Assortment"]     = ShowAssortment,
            ["Find specific items"] = () => new Page(FindSpecificItem),
            ["Ordering"]            = () => new Page(Ordering),
            ["Exchange"]            = MakeExchange
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
            ["Order by brand"] = () => {
                capturedShop.AssortmentByBrand();
                OrderedAssortmentBy("brand");
            },
            ["Order by type"] = () => {
                capturedShop.AssortmentByType();
                OrderedAssortmentBy("type");
            },
            ["Order by cost"] = () => {
                capturedShop.AssortmentByCost();
                OrderedAssortmentBy("cost");
            },
            ["Order by amount"] = () => {
                capturedShop.AssortmentByAmount();
                OrderedAssortmentBy("amount");
            },
            ["Order by producer name"] = () => {
                capturedShop.AssortmentByProducer();
                OrderedAssortmentBy("producer name");
            },
            ["Order by country"] = () => {
                capturedShop.AssortmentByCountry();
                OrderedAssortmentBy("country");
            }
        };

        #endregion

        public static void Init()
        {
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

            var backups = DataBase.RestoreBackups();
            if (backups is not null)
                capturedShops.AddRange(backups);

            Page mainPage = new Page(MainPage);
            DataBase.BackupShops(capturedShops);
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
                Console.Write("Fill randomly? true / false\t");
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
                return;
            }
            DataBase.Safe(shop);
            Console.WriteLine("The shop was created successfuly");
            Console.ReadKey();
        }

        private static void SaveShop()
        {
            Dictionary<string, Action> saveShop = null;
            saveShop = new Dictionary<string, Action>(
                capturedShops.ToDictionary(shop => shop.Name, shop => new Action(() =>
                {
                    DataBase.Safe(shop);
                    saveShop.Remove(shop.Name);
                    capturedShops.Remove(shop);
                })));
            new Page(saveShop);
        }

        private static void SaveShops()
        {
            DataBase.Safe(capturedShops);
            capturedShops.Clear();
            Console.WriteLine("All captured shops were successfuly saved to DB");
            Console.ReadKey();
        }

        private static void CaptureShop()
        {
            Dictionary<string, Action> captureShop = null;
            captureShop = new Dictionary<string, Action>(
                DataBase.GetShopsNames().ToDictionary(shop => shop, shop => new Action(() => {
                    capturedShops.Add(DataBase.ReadShopData(shop));
                    captureShop.Remove(shop);
                })));
            new Page(captureShop);
        }
        
        private static void CaptureShops()
        {
            var shopsInDB = DataBase.ReadShopsData();
            if(shopsInDB is not null)
            {
                capturedShops.AddRange(shopsInDB);
                Console.WriteLine("All shops were successfuly captured");
                Console.ReadKey();
            }
        }

        private static void Reset()
        {
            DataBase.ResetDataBase();
            Console.WriteLine("Data base were successfuly reset");
            Console.ReadKey();
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
            Dictionary<string, Action> logs = null;
            logs = new Dictionary<string, Action>(
                DataBase.GetLogFiles().ToDictionary(log => log.Name, log => new Action(() =>
                {
                    ShowLog(log.FullName);
                })));
            new Page(logs);
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
            Dictionary<string, Action> cpShops = new Dictionary<string, Action>(
                capturedShops.ToDictionary(shop => shop.Name, shop => new Action(() => 
                {
                    capturedShop = shop;
                    new Page(CapturedShopOperations);
                })));

            Page cpShopsPage = new Page(cpShops);
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
            Dictionary<string, Action> countries = new Dictionary<string, Action>(
                capturedShop.Assortment.AvailableCountries.ToDictionary(country => country, country => new Action(() =>
                {
                    foreach (var watch in capturedShop.Assortment.BrandByCountry(country))
                    {
                        Console.WriteLine(watch);
                    }
                    Console.WriteLine("\nEnter any key to go back\n");
                    Console.ReadKey();
                })));
            new Page(countries);
        }

        private static void BrandByType()
        {
            void ShowBrandsByType(WatchType type)
            {
                Console.WriteLine();
                foreach (var brand in capturedShop.Assortment.BrandByType(type))
                {
                    Console.WriteLine(brand);
                }
                Console.WriteLine("\nEnter any key to go back\n");
                Console.ReadKey();
            };

            Dictionary<string, Action> types = new Dictionary<string, Action>()
            {
                ["Quartz"]      = () => ShowBrandsByType(WatchType.Quartz),
                ["Mechanical"]  = () => ShowBrandsByType(WatchType.Mechanical)
            };

            new Page(types);
        }

        private static void MechWatchesByCostRange()
        {
            decimal cost;
            Console.Clear();
            Console.Write("Enter the cost:\t");
            while (decimal.TryParse(Console.ReadLine(), out cost) == false);
            foreach (var item in capturedShop.Assortment.MechWatchesByCostRange(cost))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\nEnter any key to go back\n");
            Console.ReadKey();
        }

        private static void ProducersByTotalCost()
        {
            decimal cost;
            Console.Clear();
            Console.WriteLine("Enter the maximum cost:\n");
            while (decimal.TryParse(Console.ReadLine(), out cost) == false);
            foreach (var item in capturedShop.Assortment.ProducersByTotalCost(cost))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\nEnter any key to go back\n");
            Console.ReadKey();
        }

        #endregion

        #region Ordering

        private static void OrderedAssortmentBy(string byWhat)
        {
            Console.WriteLine($"Assortment were successfuly ordered by {byWhat}");
            Console.ReadKey();
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

        private static void MakeExchange()
        {
            Shop buyer = capturedShop;
            Shop seller = null;
            Watch watch = null;
            int amount;

            Dictionary<string, Action> sellers = new Dictionary<string, Action>
                (capturedShops.Where(shop => shop != buyer)
                              .Select(shop => shop)
                              .ToDictionary(shop => shop.Name, shop => new Action(() =>
                              {
                                  seller = shop;
                              })));
            if(sellers.Count <= 0)
            {
                Console.WriteLine("There aren't any captured shops to make exchange");
                Console.ReadKey();
                return;
            }
            ExchangePage(sellers, "Choose the seller");
            if(seller is null)
            {
                Console.WriteLine("Undefined seller");
                Console.ReadKey();
                return;
            }


            Dictionary<string, Action> watches = new Dictionary<string, Action>
                (seller.Assortment.Watches.ToDictionary(w => w.Brand, w => new Action(() =>
                {
                    watch = w;
                })));
            if (watches.Count <= 0)
            {
                Console.WriteLine("There aren't any watches in the seller shop to make exchange");
                Console.ReadKey();
                return;
            }
            ExchangePage(watches, "Choose the watch to exchange");
            if(watch is null)
            {
                Console.WriteLine("Undefined watches");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine($"Enter the amount of watches to buy. Permissible value: {watch.Amount}");
            while (int.TryParse(Console.ReadLine(), out amount) == false);


            ExchangeEventArgs args = new ExchangeEventArgs(seller, buyer, watch, amount);
            try
            {
                Agent.MakeTransaction(buyer, args);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            void ExchangePage(Dictionary<string, Action> actions, string action)
            {
                int currentLine = 0;
                ConsoleKeyInfo keyInfo = default;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(action);
                    for (int i = 0; i < actions.Count; i++)
                    {
                        if (currentLine == i) Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(actions.ElementAt(i).Key);
                        Console.ResetColor();
                    }
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
                            if (currentLine >= 0 && currentLine < actions.Count)
                                actions?.ElementAt(currentLine).Value?.Invoke();
                            return;
                    }
                }
            }

        }
    }
}