using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchShop.WatchData;

namespace WatchShop.Factory
{
    public static class WatchFactory
    {
        #region Fields

        private static Random rng = new Random((int)DateTime.Now.Ticks);

        private static ValueRange _costRange = new ValueRange(500, 5000);
        private static ValueRange _amountRange = new ValueRange(3, 20);

        // Словарь с возможными именами часов по производителю
        private static Dictionary<(string name, string country), List<string>> _watchesBrands = new Dictionary<(string name, string country), List<string>>
        {
            [("Medioka", "Japan")] = new List<string> { "Raymond", "Ice Wall", "Fantom", "Lancer" },
            [("Huskano", "Taiwan")] = new List<string> { "Oer", "Onig", "LoeS", "Loe" },
            [("Casio", "Japan")] = new List<string> { "R1", "Ocean", "Mont D23" },
            [("Vacheron Constantin", "Switzerland")] = new List<string> { "Kallisto" },
            [("Breguet", "Switzerland")] = new List<string> { "Watch" }
        };

        #endregion

        #region Methods

        public static Watch GetWatch()
        {
            if (_watchesBrands.Count <= 0)
                return null;
               
            
            var key = _watchesBrands.Keys.ToArray()[rng.Next(0, _watchesBrands.Keys.Count)];    // Выбираем случайный ключ
            var brand = _watchesBrands[key][rng.Next(0, _watchesBrands[key].Count)];            // По ключу выбираем случайную марку часов
            var type = (WatchType)rng.Next(0, Enum.GetNames(typeof(WatchType)).Length);         // Выбираем случайный тип часов
            var cost = (decimal)_costRange.Random;                          
            var amount = (uint)_amountRange.Random;
            var producerData = new Producer(key.name, key.country);

            _watchesBrands[key].Remove(brand);      // Убираем из словаря марку часов, т.к. она уже занята
            if (_watchesBrands[key].Count <= 0)     // Если у производителя уже не осталось марок часов для создания экземпляра с уникальным именем, то удаляем производителя
                _watchesBrands.Remove(key);
                
            return new Watch(brand, type, cost, amount, producerData);
        }

        #endregion

        #region Helpers

        // Структура для описания диапазона
        private struct ValueRange
        {
            public readonly int min;
            public readonly int max;

            public ValueRange(int min, int max)
            {
                this.min = min;
                this.max = max;
            }

            public readonly int Random => rng.Next(min, max);

        }

        #endregion
    }
}
