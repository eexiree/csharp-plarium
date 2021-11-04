using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using WatchShop.Args;


namespace WatchShop.DB
{
    delegate void LogEventHandler(object source, LogEventArgs args);

    public static class DataBase
    {
        public static readonly string nl = Environment.NewLine;

        public static readonly string PathDB = Path.Combine(Environment.CurrentDirectory, "DataBase");
        public static readonly string PathLog = Path.Combine(PathDB, "Log");
        public static readonly string PathBackups = Path.Combine(PathDB, "Backups");
        

        private static readonly DirectoryInfo DBDirectory;
        private static readonly DirectoryInfo LogDirectory;
        private static readonly DirectoryInfo BackupsDirectory;

        private static (FileInfo file, StreamWriter sw, StreamReader sr) DB;
        private static (FileInfo file, StreamWriter sw, StreamReader sr) Log;

        private static bool IsDirExists(string path) => Directory.Exists(path);

        public static string GetDBData => File.ReadAllText(DB.file.FullName);

        static DataBase()
        {
            var createdTime = DateTime.Now;

            DBDirectory = CreateDirIfNotExists(PathDB);
            LogDirectory = CreateDirIfNotExists(PathLog);
            BackupsDirectory = CreateDirIfNotExists(PathBackups);

            DB.file = new FileInfo(Path.Combine(PathDB, "DB.dat"));
            if (!DB.file.Exists)
                using (DB.sw = DB.file.CreateText()) ;

            Log.file = new FileInfo(Path.Combine(PathLog, createdTime.ToString().Replace(':', '.') + ".log"));
            using (Log.sw = Log.file.CreateText())
            {
                Log.sw.WriteLine(createdTime.ToString().PadRight(30, '-') + "LOG CREATED " + createdTime.GetHashCode().ToString("X"));
            }
        }

        private static DirectoryInfo CreateDirIfNotExists(string path) =>
            IsDirExists(path) ? new DirectoryInfo(path) : Directory.CreateDirectory(path);

        #region Save

        public static void Safe(Shop shop, bool append = true)
        {
            DB.sw = append ? DB.file.AppendText() : DB.file.CreateText();
            using (DB.sw)
            {
                DB.sw.Write(shop);
            }
        }

        public static void Safe(List<Shop> shops, bool append = true)
        {
            foreach(var shop in shops)
            {
                Safe(shop, append);
            }
        }

        #endregion

        #region Read

        public static Shop ReadShopData(string shopName)
        {
            Shop shop = null;
            List<Watch> watches = new List<Watch>();

            decimal shopMoney = default;
            bool isShopFind = false;

            StringBuilder rewriten = new StringBuilder();

            using (DB.sr = new StreamReader(DB.file.OpenRead()))
            {
                string line;
                while (!DB.sr.EndOfStream)
                {
                    line = DB.sr.ReadLine();
                    if (line.Contains("Shop") && !line.Contains(shopName))
                    {
                        isShopFind = false;
                        rewriten.Append(line + Environment.NewLine);
                    }
                    else if (line.Contains("Shop") && line.Contains(shopName))
                    {
                        isShopFind = true;
                        if (!decimal.TryParse(DB.sr.ReadLine().GetValue(), out shopMoney))
                            throw new InvalidCastException($"Cannot parse money value");
                        shop = new Shop(shopName, shopMoney);
                    }
                    else if (line.Contains("Brand") && isShopFind)
                    {
                        var brand = line.GetValue();
                        var type  = DB.sr.ReadLine().GetValue() == "Quartz" ? WatchType.Quartz : WatchType.Mechanical;

                        decimal cost;
                        if (!decimal.TryParse(DB.sr.ReadLine().GetValue(), out cost))
                            throw new InvalidCastException($"Cannot parse cost value");

                        int amount;
                        if (!int.TryParse(DB.sr.ReadLine().GetValue(), out amount))
                            throw new InvalidCastException($"Cannot parse amount value");

                        string temp = DB.sr.ReadLine().GetValue();
                        var producer = temp.Substring(0, temp.IndexOf('-'));
                        var country  = temp.GetValue('-');

                        watches.Add(new Watch(brand,
                                              type,
                                              cost,
                                              amount,
                                              new Producer(producer, country)));
                    }
                    else if (!isShopFind) 
                        rewriten.Append(line + Environment.NewLine);
                }
            }

            using (DB.sw = DB.file.CreateText())
            {
                DB.sw.Write(rewriten);
            }

            shop?.Assortment.AddRange(watches);
            return shop;
        }

        public static List<Shop> ReadShopsData()
        {
            List<Shop> shops = new List<Shop>();
            foreach(var shopName in GetShopsNames())
            {
                shops.Add(ReadShopData(shopName));
            }
            return shops;
        }

        #endregion

        public static void OnLogRequest(object source, LogEventArgs args)
        {
            var timeSection = args.RequestTime.ToUniversalTime().ToString().PadRight(30, '-');
            var argsSection = source.GetType().Name + " invoke " + args.MethodName + " with " + args.Argument.GetType().Name + ". Is successful: " + args.IsSuccessful;
            using (Log.sw = Log.file.AppendText())
            {
                Log.sw.WriteLine(timeSection + argsSection);
            }
        }



        //public static void ChangeFieldValue(string shop, string brand, Fields field, object value)
        //{
        //    StringBuilder rewrited = new StringBuilder();
        //    using (DB.sr = new StreamReader(DB.file.OpenRead()))
        //    {
        //        string line;
        //        bool isShopFind = false, isBrandFind = false;
        //        while (!DB.sr.EndOfStream)
        //        {
        //            line = DB.sr.ReadLine();
        //            if (line.Contains(shop))
        //                isShopFind = true;
        //            else if (line.Contains(brand) && isShopFind)
        //                isBrandFind = true;
        //            else if (line.Contains(field.ToString()) && isBrandFind)
        //            {
        //                rewrited.Append(line.GetField() + value.ToString() + Environment.NewLine);
        //                isShopFind = isBrandFind = false;
        //                continue;
        //            }
        //            rewrited.Append(line + Environment.NewLine);
        //        }
        //    }
        //    using (DB.sw = DB.file.CreateText())
        //    {
        //        DB.sw.Write(rewrited);
        //    }
        //}

        //public static void SerializeNonSaved(Shop shop)
        //{
        //    FileInfo jsonFile = new FileInfo(Path.Combine(_DBDir.FullName, shop.Name + ".json"));
        //    using (StreamWriter jsonWriter = jsonFile.CreateText())
        //    {
        //        jsonWriter.WriteAsync(JsonSerializer.Serialize(shop));
        //        using (HaveToBeRestored.sw = HaveToBeRestored.file.AppendText())
        //        {
        //            HaveToBeRestored.sw.WriteAsync(jsonFile.Name + Environment.NewLine);
        //        }
        //    }
        //}

        #region Backups

        public static void BackupShop(Shop shop)
        {
            FileInfo jsonFile = new FileInfo(Path.Combine(PathBackups, shop.Name + ".json"));
            using (StreamWriter jsonWriter = jsonFile.CreateText())
            {
                jsonWriter.Write(JsonSerializer.Serialize(shop));
            }
        }

        public static void BackupShops(List<Shop> shops)
        {
            foreach(var shop in shops)
            {
                BackupShop(shop);
            }
        }

        public static List<Shop> RestoreBackups()
        {
            List<Shop> shops = new List<Shop>();
            if (IsDirExists(BackupsDirectory.FullName) && BackupsDirectory.GetFiles().Length > 0)
            {
                foreach (var jsonFile in BackupsDirectory.GetFiles())
                {
                    string jsonString = File.ReadAllText(jsonFile.FullName);
                    Shop shop = JsonSerializer.Deserialize<Shop>(jsonString);
                    shops.Add(shop);
                    jsonFile.Delete();
                }
                return shops;
            }
            else return null;
        }

        #endregion

        #region Other

        public static void ResetDataBase()
        {
            DB.file.Delete();
            DB.file = new FileInfo(Path.Combine(DBDirectory.FullName, "DB.dat"));
            using (DB.sw = DB.file.CreateText()) ;
        }

        public static List<string> GetShopsNames()
        {
            List<string> shops = new List<string>();
            using (DB.sr = new StreamReader(DB.file.OpenRead()))
            {
                string line;
                while (!DB.sr.EndOfStream)
                {
                    line = DB.sr.ReadLine();
                    if (line.Contains("Shop"))
                        shops.Add(line.GetValue());
                }
            }
            return shops;
        }
        public static FileInfo[] GetLogFiles()
        {
            return LogDirectory?.GetFiles();
        }

        #endregion

        public enum Fields
        {
            Cost, Amount
        }
    }

    public static class DBExtension
    {
        public static string GetValue(this string line, char lastChar = '.') => line.Substring(line.LastIndexOf(lastChar) + 1);
        public static string GetField(this string line, char lastChar = '.') => line.Substring(0, line.LastIndexOf(lastChar) + 1);
    }
}
