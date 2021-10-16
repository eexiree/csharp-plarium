namespace Clock_Shop
{
    public class Clock
    {
        public string Brand     // Свойства, присвоение значений которым, возможна только в конструкторе (в данном случае) из-за приватного сеттера
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

        public Clock(string brand, ClockType type, decimal cost, int amount, Producer details)  // Конструктор
        {
            Brand = brand;
            Type = type;
            Cost = cost;
            Amount = amount;
            Details = details;
        }

        public override string ToString()   // Переопределенный метод преобразования типа в строку
        {
            return $"\n-------------------\nБренд: {Brand}\nТип: {Type}\nЦена: {Cost}\nКол-во в магазине: {Amount}\n\nРеквизиты производителя: {Details}\n-------------------\n";
        }
    }

    public enum ClockType   // Перечисление типа часов
    {
        Quartz, Mechanical
    }

}
