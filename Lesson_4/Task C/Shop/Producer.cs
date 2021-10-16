namespace Clock_Shop
{
    public class Producer   // Класс производителя, в котором хранится информация названия производителя и страны производства
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
