using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock_Shop
{
    public class Clock
    {
        public string Brand
        {
            get;
            private set;
        }
        public ClockType Type
        {
            get;
            private set;
        }
        public decimal Cost
        {
            get;
            private set;
        }
        public int Amount
        {
            get;
            private set;
        }
        public Producer Details
        {
            get;
            private set;
        }

        public Clock(string brand, ClockType type, decimal cost, int amount, Producer details)
        {
            Brand = brand;
            Type = type;
            Cost = cost;
            Amount = amount;
            Details = details;
        }

        public override string ToString()
        {
            return $"\n-------------------\nБренд: {Brand}\nТип: {Type}\nЦена: {Cost}\nКол-во в магазине: {Amount}\n\nРеквизиты производителя: {Details}\n-------------------\n";
        }
    }

    public enum ClockType 
    {
        Quartz, Mechanical
    }

}
