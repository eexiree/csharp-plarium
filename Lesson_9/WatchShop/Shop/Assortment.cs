﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatchShop.Args;
using WatchShop.DB;

namespace WatchShop
{
    delegate void SortEventHandler(object source, SortEventArgs args);

    [Serializable] public class Assortment
    {
        #region Fields

        private List<Watch> _watches;   // Коллекция часов 
        public List<Watch> Watches
        {
            get => _watches;
            set
            {
                _watches = value;
            }
        }

        private HashSet<string> _availableCountries = new HashSet<string>();    // Хеш-сет, который хранит страны производителей, исходя из коллекции часов 
        public HashSet<string> AvailableCountries
        {
            get => _availableCountries;
            set
            {
                _availableCountries = value;
            }
        }
        public bool IsCountryAvailable(string country) => _availableCountries.Contains(country);
        
        public Action<object, LogEventArgs> Handler = DataBase.OnLogRequest;

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

            OnRequest(new LogEventArgs("Add", watch, true, DateTime.Now));
        }

        public void AddRange(IEnumerable<Watch> watchCollection)
        {
            _watches.AddRange(watchCollection);

            foreach (var w in watchCollection)
                _availableCountries.Add(w.ProducerData.Country);

            OnRequest(new LogEventArgs("AddRange", watchCollection, true, DateTime.Now));
        }

        public bool Remove(Watch watch)
        {
            var isSuccessful = _watches.Remove(watch);

            OnRequest(new LogEventArgs("Remove", watch, isSuccessful, DateTime.Now));

            return isSuccessful;
        }

        public void RemoveAt(int index)
        {
            _watches.RemoveAt(index);
            OnRequest(new LogEventArgs("RemoveAt", index, true, DateTime.Now));
        }

        public bool Contains(string brand)
        {
            bool isSuccessful = false;
            foreach (var watch in _watches)
                if (watch.Brand == brand)
                    isSuccessful = true;
            OnRequest(new LogEventArgs("Contains", brand, isSuccessful, DateTime.Now));
            return isSuccessful;
        }

        public int IndexOf(string brand)
        {
            int index = -1;
            for(int i = 0; i < _watches.Count; i++)
                if (_watches[i].Brand == brand)
                    index = i;
            OnRequest(new LogEventArgs("IndexOf", brand, index >= 0 ? true : false, DateTime.Now));
            return index;
        }

        public void FillIn(int amount)
        {
            if (amount <= 0)
                return;
            Watch watch = null;
            bool isSuccessful = true;
            for (int i = 0; i < amount; i++)
            {
                watch = Factory.GetWatch();
                if (watch is null)
                {
                    isSuccessful = i == 0 ? false : true;
                    break;
                }
                    
                _watches.Add(watch);
                _availableCountries.Add(watch.ProducerData.Country);
            }
            OnRequest(new LogEventArgs("FillIn", amount, isSuccessful, DateTime.Now));
        }

        public void OrderBy(object source, SortEventArgs args)
        {
            bool isSuccessful = true;
            try
            {
                _watches.Sort(args.Comparison);
            }
            catch(Exception ex)
            {
                isSuccessful = false;
            }

            OnRequest(new LogEventArgs("OrderBy", (source, args), isSuccessful, DateTime.Now));
        }

        private void OnRequest(LogEventArgs args)
        {
            Handler?.Invoke(this, args);
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
            string nl = Environment.NewLine;
            StringBuilder output = new StringBuilder();
            foreach (var watch in _watches)
            {
                output.Append(watch);
            }
            output.Append($"{nl}{nl}");
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
