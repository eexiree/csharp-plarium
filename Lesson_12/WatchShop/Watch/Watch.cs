using System;

namespace WatchShop { 

    [Serializable]
    public class Watch
    {

        #region Fields
        public string Brand
        {
            get;
            set;
        }
        public WatchType Type
        {
            get;
            set;
        }
        public decimal Cost
        {
            get;
            set;
        }
        public int Amount
        {
            get;
            set;
        }
        public Producer ProducerData
        {
            get;
            set;
        }
        #endregion

        #region Constructors

        public Watch(string brand, WatchType type, decimal cost, int amount, Producer producerData)
        {
            Brand = brand;
            Type = type;
            Cost = cost;
            Amount = amount;
            ProducerData = producerData;
        }

        public Watch(Watch other)
        {
            Brand = new string(other.Brand);
            Type = other.Type;
            Cost = other.Cost;
            Amount = other.Amount;
            ProducerData = new Producer(other.ProducerData);
        }

        public Watch()
        {
            Brand = "None";
            Cost = 0;
            Amount = 0;
            ProducerData = new Producer();
        }

        #endregion

        public override string ToString()
        {
            string nl = Environment.NewLine;
            return $"{nl}Brand".PadRight(20, '.') + Brand +
                   $"{nl}Type".PadRight(20, '.') + Type +
                   $"{nl}Cost".PadRight(20, '.') + Cost +
                   $"{nl}Amount".PadRight(20, '.') + Amount +
                   $"{nl}Producer data".PadRight(20, '.') + ProducerData.Name + "---" + ProducerData.Country + nl;
        }

    }

    public enum WatchType
    {
        Quartz, Mechanical
    }
}
