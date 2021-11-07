using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatchShop.WatchData;
using WatchShop.Factory;

namespace WatchShop
{
    public class WatchCollection : IEnumerable
    {
        #region Fields

        private List<Watch> _watches;   // Коллекция часов 
        private HashSet<string> _availableCountries = new HashSet<string>();    // Хеш-сет, который хранит страны производителей, исходя из коллекции часов 

        public bool IsCountryAvailable(string country) => _availableCountries.Contains(country);

        #endregion

        #region Constructors

        public WatchCollection(int capacity)
        {
            _watches = new List<Watch>(capacity);
        }
        public WatchCollection()
        {
            _watches = new List<Watch>();
        }
        public WatchCollection(WatchCollection other)
        {
            _watches = new List<Watch>(other._watches);
        }

        #endregion

        #region Methods

        public void Add(Watch watch)
        {
            _watches.Add(watch);
        }

        public void Remove(Watch watch)
        {
            _watches.Remove(watch);
        }

        public void RemoveAt(int index)
        {
            _watches.RemoveAt(index);
        }

        // Метод заполнения коллекции посредством фабрики
        public void FillIn(int amount)
        {
            Watch watch = null;
            for(int i = 0; i < amount; i++)
            {
                watch = WatchFactory.GetWatch();
                if (watch is null)
                    break;
                _watches.Add(watch);
                _availableCountries.Add(watch.ProducerData.Country);
            }
        }

        public IEnumerable BrandByType(WatchType type) => from watch in _watches where watch.Type == type select watch.Brand;
        public IEnumerable MechWatchesByCostRange(decimal max) => from watch in _watches where watch.Type == WatchType.Mechanical && watch.Cost < max select watch;
        public IEnumerable BrandByCountry(string country) 
        {
            if (IsCountryAvailable(country))
                return from watch in _watches where watch.ProducerData.Country == country select watch.Brand;
            else return null;
        }
        public IEnumerable ProducersByTotalCost(decimal totalCost)
        {
            foreach(var prod in Producers())
            {
                decimal cost = 0;
                foreach (var watch in _watches)
                {
                    if (watch.ProducerData.Name == prod)
                        cost += watch.Amount * watch.Cost;
                }
                if(cost <= totalCost)
                    yield return prod;
            }
        }
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach(var watch in _watches)
            {
                output.Append(watch);
            }
            return output.ToString();
        }

        private List<string> Producers()
        {
            HashSet<string> prods = new HashSet<string>();
            foreach(var watch in _watches)
            {
                prods.Add(watch.ProducerData.Name);
            }
            return prods.ToList();
        }

        #endregion

        public IEnumerator GetEnumerator() => _watches.GetEnumerator();
        public Watch this[int index] => _watches[index];
    }
}
