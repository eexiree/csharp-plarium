using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WatchShop.Args;

namespace WatchShop.DB
{
    delegate void LogEventHandler(object source, LogEventArgs args);

    // Статический класс базы данных 
    public static class DataBase
    {
        #region Fields

        public static readonly string nl = Environment.NewLine;

        public static readonly string PathDB = Path.Combine(Environment.CurrentDirectory, "DataBase");  // Путь к базе данных
        public static readonly string PathLog = Path.Combine(PathDB, "Log");                            // Путь к директории с логами
        public static readonly string PathBackups = Path.Combine(PathDB, "Backups");                    // Путь к директории с резервными копиями
        

        private static readonly DirectoryInfo DBDirectory;      // Директория БД
        private static readonly DirectoryInfo LogDirectory;     // Директория с логами
        private static readonly DirectoryInfo BackupsDirectory; // Директория с резервными копиями

        private static (FileInfo file, StreamWriter sw, StreamReader sr) DB;
        private static (FileInfo file, StreamWriter sw) Log;

        private static bool IsDirExists(string path) => Directory.Exists(path);
        public static string GetDBData => File.ReadAllText(DB.file?.FullName);
        public static string[] GetDBDataAllLines => File.ReadAllLines(DB.file?.FullName);

        private static object lockThread = new object();

        #endregion

        #region Static Constructor

        // Статический конструктор
        static DataBase()
        {
            var createdTime = DateTime.Now;

            DBDirectory = CreateDirIfNotExists(PathDB);             // Создаем директории, если они отсутствуют
            LogDirectory = CreateDirIfNotExists(PathLog);
            BackupsDirectory = CreateDirIfNotExists(PathBackups);

            DB.file = new FileInfo(Path.Combine(PathDB, "DB.dat"));
            if (!DB.file.Exists)    // Если файл БД не создан - создаем
                using (DB.sw = DB.file.CreateText());

            Log.file = new FileInfo(Path.Combine(PathLog, createdTime.ToString().Replace(':', '.') + ".log"));
            using (Log.sw = Log.file.CreateText())  // Создаем файл лога текущей сессии
            {
                Log.sw.WriteLine(createdTime.ToString().PadRight(30, '-') + "LOG CREATED " + createdTime.GetHashCode().ToString("X"));
            }
        }

        #endregion

        #region Writing methods to the DB

        public static void Safe(Shop shop, bool append = true)          // Метод, который сохраняет в БД магазин часов
        {
            new Thread(() => WriteShopDataToDB(shop, append)).Start();  // Создаем новый поток, через лямбда-выражение определяем метод записи в файл БД с аргументами магазина, который необходимо сохранить
        }
        public static void Safe(List<Shop> shops, bool append = true)   // Перегруженный метод сохранения в БД списка магазинов
        {
            foreach(var shop in shops)
            {
                Safe(shop, append);
            }
        }

        public static void Rewrite(string str)  // Метод для перезаписи данных в файле БД
        {
            new Thread(() =>
            {
                lock (lockThread)
                {
                    using (DB.sw = DB.file.CreateText())
                    {
                        DB.sw.Write(str);
                    }
                }
            }).Start();
        }

        private static void WriteShopDataToDB(Shop shop, bool append)   // Метод, который записывает данные магазина в файл БД
        {
            lock (lockThread)
            {
                DB.sw = append ? DB.file.AppendText() : DB.file.CreateText();
                using (DB.sw)
                {
                    DB.sw.Write(shop);
                }
            }
        }

        #endregion



        #region Reading methods from the DB

        public static void ReadShopData(string shopName)
        {
            lock (lockThread)
            {
                bool isSuccessful = true;   // Результат выполнения
                string msg = "None";        

                try
                {
                    Task<Shop> shopTask = new Task<Shop>(GetShopFromDB, shopName);  // Создаем новый таск
                    shopTask.ContinueWith((taskRes) =>                              // Определяем метод продолжения
                    {
                        UI.UserInterface.AddCapturedShops(taskRes.Result);
                    });
                    shopTask.Start();   // Начинаем таску
                }
                catch (Exception ex)
                {
                    isSuccessful = false;
                    msg = ex.Message;
                }
                finally
                {
                    LogEventArgs args = new LogEventArgs("Read shop data",
                                                             msg,
                                                             isSuccessful,
                                                             DateTime.Now);
                    WriteLog("UI", args);
                }
            }
        }

        public static void ReadShopsData()
        {
            foreach (var shopName in GetShopsNames())
            {
                ReadShopData(shopName);
            }
        }

        // Не найлучшая реализация получения данных магазина из файла БД
        private static Shop GetShopFromDB(object _shopName)
        {
            if (!GetShopsNames().Contains(_shopName as string))
                return null;
            lock (lockThread)
            {
                string shopName = _shopName as string;
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
                            var type = DB.sr.ReadLine().GetValue() == "Quartz" ? WatchType.Quartz : WatchType.Mechanical;

                            decimal cost;
                            if (!decimal.TryParse(DB.sr.ReadLine().GetValue(), out cost))
                                throw new InvalidCastException($"Cannot parse cost value");

                            int amount;
                            if (!int.TryParse(DB.sr.ReadLine().GetValue(), out amount))
                                throw new InvalidCastException($"Cannot parse amount value");

                            string temp = DB.sr.ReadLine().GetValue();
                            var producer = temp.Substring(0, temp.IndexOf('-'));
                            var country = temp.GetValue('-');

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
        }

        #endregion

        #region Writing Log methods

        public static void OnLogRequest(object source, LogEventArgs args)
        {
            new Thread(() => { WriteLog(source, args); }).Start();
        }

        private static void WriteLog(object source, LogEventArgs args)  // Метод записи лога в файл лога текущей сессии
        {
            lock(lockThread)
            { 
                var timeSection = args.RequestTime.ToUniversalTime().ToString().PadRight(30, '-');
                var argsSection = source +
                                    " invoke " + args.MethodName +
                                    " with " + args.Argument +
                                    ". Is successful: " + args.IsSuccessful + 
                                    ". Execution thread: " + Thread.CurrentThread.ManagedThreadId;
                using (Log.sw = Log.file.AppendText())
                {
                    Log.sw.WriteLine(timeSection + argsSection);
                }
            }
        }

        #endregion

        #region Backups maker

        #region Make Backup

        public static void BackupShop(Shop shop)    // Метод создания файла резервной копии
        {
            FileInfo jsonFile = new FileInfo(Path.Combine(PathBackups, shop.Name + ".json"));
            using (StreamWriter jsonWriter = jsonFile.CreateText())
            {
                jsonWriter.Write(JsonSerializer.Serialize(shop));
            }
        }

        public static void BackupShops(List<Shop> shops)    // Метод, который создает резервные копии списка магазинов
        {
            foreach(var shop in shops)
            {
                BackupShop(shop);
            }
        }

        #endregion

        #region Restore Backups

        public static void RestoreBackups()
        {
            var backupFilesAmount = BackupsDirectory.GetFiles().Length;
            var recoveredFilesCounter = 0;
            foreach(var jsonFile in BackupsDirectory.GetFiles())
            {
                bool isSuccessful = true;
                string arg = "None";
                try
                {
                    Task<Shop> shopTask = new Task<Shop>(RestoreBackup, jsonFile);

                    shopTask.ContinueWith((taskRes) =>
                    {
                        UI.UserInterface.AddCapturedShops(taskRes.Result);
                    });

                    shopTask.Start();
                    recoveredFilesCounter++;
                }
                catch(Exception ex)
                {
                    isSuccessful = false;
                    arg = ex.Message;
                }
                finally
                {
                    LogEventArgs args = new LogEventArgs("Restore Backups Async",
                                                         arg,
                                                         isSuccessful,
                                                         DateTime.Now);
                    WriteLog("UI", args);
                }
            }
            if (backupFilesAmount != recoveredFilesCounter) 
                throw new JsonException($"Not all of the backups were restored");
        }

        private static Shop RestoreBackup(object _jsonFile) // Метод востановления резервной копий
        {
            FileInfo jsonFile = _jsonFile as FileInfo;
            string jsonString = File.ReadAllText(jsonFile.FullName);
            Shop shop = new Shop(JsonSerializer.Deserialize<Shop>(jsonString));
            jsonFile.Delete();
            return shop;
        }

        #endregion

        #endregion

        #region Helpers

        private static DirectoryInfo CreateDirIfNotExists(string path) =>
            IsDirExists(path) ? new DirectoryInfo(path) : Directory.CreateDirectory(path);

        public static void ResetDataBase()
        {
            DB.file.Delete();
            DB.file = new FileInfo(Path.Combine(DBDirectory.FullName, "DB.dat"));
            using (DB.sw = DB.file.CreateText());
        }

        public static List<string> GetShopsNames()  // Метод для получения названий магазинов в БД
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
    }

    public static class DBExtension
    {
        public static string GetValue(this string line, char lastChar = '.') => line.Substring(line.LastIndexOf(lastChar) + 1);
        public static string GetField(this string line, char lastChar = '.') => line.Substring(0, line.LastIndexOf(lastChar) + 1);
    }
}
