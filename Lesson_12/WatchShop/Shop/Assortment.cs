using System;
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
        public IEnumerable<string> AvailableCountries => _watches.Select(watch => watch.ProducerData.Country).Distinct();   // Страны производителей часов

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
            if (Contains(watch.Brand))
                this[watch.Brand].Amount += watch.Amount;
            else
                _watches.Add(watch);

            OnRequest(new LogEventArgs("Add", watch, true, DateTime.Now));
        }

        public void AddRange(IEnumerable<Watch> watchCollection)
        {
            foreach (var watch in watchCollection)
                Add(watch);
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
                Console.WriteLine(ex.Message);
                Console.ReadKey();
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
        public IEnumerable BrandByType(WatchType type) => _watches.Where(watch => watch.Type == type).Select(watch => watch.Brand);
        public IEnumerable MechWatchesByCostRange(decimal max) => _watches.Where(watch => watch.Type == WatchType.Mechanical && watch.Cost < max);
        public IEnumerable BrandByCountry(string country) => _watches.Where(watch => watch.ProducerData.Country == country)
                                                                     .Select(watch => watch.Brand);
        public IEnumerable ProducersByTotalCost(decimal totalCost) => _watches.Where(watch => (watch.Cost * watch.Amount) <= totalCost)
                                                                              .Select(watch => watch.ProducerData.Name);

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
                    return this[IndexOf(brand)];
                else return null;
            }
        }

        #endregion
    }
}
