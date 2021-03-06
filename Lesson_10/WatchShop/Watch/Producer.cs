namespace WatchShop
{
    [System.Serializable]
    public class Producer
    {

        #region Fields
        public string Name
        {
            get;
            set;
        }
        public string Country
        {
            get;
            set;
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
            Name = new string(other?.Name);
            Country = new string(other?.Country);
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
