using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchShop.WatchData
{
    public class Producer
    {
        #region Fields
        public string Name
        {
            get;
            private set;
        }
        public string Country
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public Producer(string name, string country)
        {
            Name = name;
            Country = country;
        }
        public Producer(Producer other)
        {
            Name = other.Name;
            Country = other.Country;
        }
        public Producer()
        {
            Name = "Unknown";
            Country = "Unknown";
        }
        

        #endregion

        #region Overriden

        public override string ToString()
        {
            return $"\nИзготовитель: {Name}\nСтрана изготовителя: {Country}";
        }

        #endregion
    }
}
