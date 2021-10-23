using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchShop.WatchData
{
    public class Watch
    {
        #region Fields
        public string Brand     
        {
            get;
            private set;
        }
        public WatchType Type
        {
            get;
            private set;
        }
        public decimal Cost
        {
            get;
            private set;
        }
        public uint Amount
        {
            get;
            private set;
        }
        public Producer ProducerData
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        
        public Watch(string brand, WatchType type, decimal cost, uint amount, Producer producerData)  // Конструктор
        {
            Brand = brand;
            Type = type;
            Cost = cost;
            Amount = amount;
            ProducerData = producerData;
        }

        public Watch(Watch other)
        {
            Brand = other.Brand;
            Type = other.Type;
            Cost = other.Cost;
            Amount = other.Amount;
            ProducerData = ProducerData;
        }

        public Watch()
        {
            Brand = "None";
            Cost = 0;
            Amount = 0;
            ProducerData = null;
        }
        
        #endregion

        public override string ToString()
        {
            return $"\n-------------------\nБренд: {Brand}\nТип: {Type}\nЦена: {Cost}\nКол-во в магазине: {Amount}\n\nРеквизиты производителя: {ProducerData}\n-------------------\n";
        }
    }

    public enum WatchType
    {
        Quartz, Mechanical
    }
}
