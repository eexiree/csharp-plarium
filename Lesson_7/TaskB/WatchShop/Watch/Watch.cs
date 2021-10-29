namespace WatchShop { 
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
        public int Amount
        {
            get;
            set;
        }
        public Producer ProducerData
        {
            get;
            private set;
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
