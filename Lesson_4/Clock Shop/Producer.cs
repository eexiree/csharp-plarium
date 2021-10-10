using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock_Shop
{
    public class Producer
    {
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

        public Producer(string name, string country)
        {
            Name = name;
            Country = country;
        }

        public override string ToString()
        {
            return $"\nИзготовитель: {Name}\nСтрана изготовителя: {Country}";
        }
    }
}
