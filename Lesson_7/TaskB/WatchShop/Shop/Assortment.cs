using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatchShop
{
    delegate void SortEventHandler(object source, SortEventArgs args);

    public class Assortment : IEnumerable
    {
        #region Fields

        private List<Watch> _watches;   // Коллекция часов 
        private HashSet<string> _availableCountries = new HashSet<string>();    // Хеш-сет, который хранит страны производителей, исходя из коллекции часов 

        public bool IsCountryAvailable(string country) => _availableCountries.Contains(country);

        #endregion

        #region Constructors

        public Assortment(int capacity)
        {
            _watches = new List<Watch>(capacity);
        }
        public Assortment()
        {
            _watches = new List<Watch>();
        }
        public Assortment(Assortment other)
        {
            _watches = new List<Watch>(other._watches);
        }

        #endregion

        #region Methods

        #region Collection Methods

        public void Add(Watch watch)
        {
            _watches.Add(watch);
        }

        public bool Remove(Watch watch)
        {
            return _watches.Remove(watch);
        }

        public void RemoveAt(int index)
        {
            _watches.RemoveAt(index);
        }

        public bool Contains(string brand)
        {
            foreach (var watch in _watches)
                if (watch.Brand == brand)
                    return true;
            return false;
        }

        public int IndexOf(string brand)
        {
            for(int i = 0; i < _watches.Count; i++)
                if (_watches[i].Brand == brand)
                    return i;
            return -1;
        }

        public void FillIn(int amount)
        {
            Watch watch = null;
            for (int i = 0; i < amount; i++)
            {
                watch = Factory.GetWatch();
                if (watch is null)
                    break;
                _watches.Add(watch);
                _availableCountries.Add(watch.ProducerData.Country);
            }
        }

        public void OrderBy(object source, SortEventArgs args)
        {
            _watches.Sort(args.Comparison);
        }

        #endregion

        #region Tasks Methods

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
            foreach (var prod in Producers())
            {
                decimal cost = 0;
                foreach (var watch in _watches)
                {
                    if (watch.ProducerData.Name == prod)
                        cost += watch.Amount * watch.Cost;
                }
                if (cost <= totalCost)
                    yield return prod;
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (var watch in _watches)
            {
                output.Append(watch);
            }
            return output.ToString();
        }

        private List<string> Producers()
        {
            HashSet<string> prods = new HashSet<string>();
            foreach (var watch in _watches)
            {
                prods.Add(watch.ProducerData.Name);
            }
            return prods.ToList();
        }

        #endregion

        #region Indexers & Enumerator

        public IEnumerator GetEnumerator() => _watches.GetEnumerator();

        public Watch this[int index]
        {
            get
            {
                if (index < _watches.Count)
                    return _watches[index];
                else
                    return null;
            }
        }
        public Watch this[string brand]
        {
            get
            {
                if (Contains(brand))
                    return new Watch(_watches[IndexOf(brand)]);
                else return null;
            }
        }

        #endregion
    }
}
