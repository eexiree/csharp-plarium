using System;
using WatchShop.Args;

namespace WatchShop
{
    public static class Agent
    {
        public delegate void ExchangeEventHandler(object sender, ExchangeEventArgs args);

        private static event ExchangeEventHandler OnExchange;

        public static void MakeTransaction(object sender, ExchangeEventArgs args)
        {
            if (args.Watch == null)
                throw new NullReferenceException("Watch were null");

            else if (!args.Seller.Assortment.Contains(args.Watch.Brand))
                throw new ArgumentException($"There is no watches in {args.Seller.Name}");

            else if (args.Seller is null || args.Buyer is null)
                throw new ArgumentException("Buyer OR Seller is null");

            else if (args.Buyer.Equals(args.Seller))
                throw new ArgumentException("Buyer == Seller");

            else if (args.Buyer.Money < args.TotalCost)
                throw new ArgumentException("Not enough money");

            else if (args.Seller.Assortment[args.Watch.Brand]?.Amount < args.Amount)
                throw new ArgumentException("Not enough watches");

            else Exchange(sender, args);
        }

        private static void Exchange(object sender, ExchangeEventArgs args)
        {
            OnExchange?.Invoke(sender, args);
        }

        public static void Subsribe(ExchangeEventHandler m)
        {
            OnExchange += m;
        }
    }
}
