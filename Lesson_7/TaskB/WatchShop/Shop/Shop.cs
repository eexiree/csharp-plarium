using System;

namespace WatchShop
{
    public class Shop
    {

        #region Fields

        private SortEventHandler orderBy;

        public string Name
        {
            get;
            private init;
        }
        public decimal Money
        {
            get;
            private set;
        }

        
        public readonly Assortment Assortment = new Assortment();

        #endregion

        #region Constructor

        public Shop(string name, int assortmentAmount, decimal money)
        {
            Name = name;
            Money = money;
            Assortment.FillIn(assortmentAmount);
            orderBy = new SortEventHandler(Assortment.OrderBy);

            Agent.Subsribe(AgentListener);
        }

        #endregion

        #region Assortment Comparisons


        public void AssortmentByCost()
        {
            SortEventArgs arg = new SortEventArgs();
            arg.Comparison = (Watch a, Watch b) =>
            {
                if (a.Cost > b.Cost)
                    return 1;
                else if (a.Cost < b.Cost)
                    return -1;
                else return 0;
            };
            orderBy?.Invoke(this, arg);
        }

        public void AssortmentByType()
        {
            SortEventArgs arg = new SortEventArgs();
            arg.Comparison = (Watch a, Watch b) =>
            {
                if (a.Type > b.Type)
                    return 1;
                else if (a.Type < b.Type)
                    return -1;
                else return 0;
            };
            orderBy?.Invoke(this, arg);
        }

        public void AssortmentByAmount()
        {
            SortEventArgs arg = new SortEventArgs()
            {
                Comparison = (Watch a, Watch b) =>
                {
                    if (a.Amount > b.Amount)
                        return 1;
                    else if (a.Amount < b.Amount)
                        return -1;
                    else return 0;
                }
            };
            orderBy?.Invoke(this, arg);
        }

        public void AssortmentByCountry()
        {
            SortEventArgs arg = new SortEventArgs()
            {
                Comparison = (Watch a, Watch b) =>
                {
                    return String.Compare(a.ProducerData.Country, b.ProducerData.Country);
                }
            };
            orderBy?.Invoke(this, arg);
        }

        public void AssortmentByProducer()
        {
            SortEventArgs arg = new SortEventArgs()
            {
                Comparison = (Watch a, Watch b) =>
                {
                    return String.Compare(a.ProducerData.Name, b.ProducerData.Name);
                }
            };
            orderBy?.Invoke(this, arg);
        }

        public void AssortmentByBrand()
        {
            SortEventArgs arg = new SortEventArgs()
            {
                Comparison = (Watch a, Watch b) =>
                {
                    return String.Compare(a.Brand, b.Brand);
                }
            };
            orderBy?.Invoke(this, arg);
        }


        #endregion

        #region Shops Interaction

        private void AgentListener(object sender, ExchangeEventArgs args)
        {   
            if(args.Buyer == this)
            {
                Buy(args);
            }
            else if(args.Seller == this)
            {
                Sell(args);
            }
        }

        private void Buy(ExchangeEventArgs args)
        {
            Money -= args.TotalCost.Value;
            args.Seller.AddMoney(args.TotalCost.Value);
        }

        private void Sell(ExchangeEventArgs args)
        {
            Watch temp = Assortment[Assortment.IndexOf(args.Watch.Brand)];
            if (temp.Amount <= 0)
                Assortment.Remove(temp);
            temp.Amount -= args.Amount;
            args.Buyer.Assortment.Add(new Watch(temp) { Amount = args.Amount });
        }

        public void AddMoney(decimal amount)
        {
            Money += amount;
        }

        #endregion

        public override string ToString()
        {
            return $"Shop {Name}\nMoney: {Money}\n{Assortment.ToString()}";
        }
    }
}
