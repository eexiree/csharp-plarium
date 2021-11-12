using System;
using System.Collections.Generic;
using WatchShop.Args;

namespace WatchShop
{
    public delegate void ExchangeEventHandler(object sender, ExchangeEventArgs args);
    
    // Класс который является посредником между магазинами, для совершения обмена между ними часами
    public static class Agent
    {
        
        private static event ExchangeEventHandler OnExchange;   // Событие типа делегата ExchangeEventHandler

        public static void MakeTransaction(object sender, ExchangeEventArgs args)
        {
            if (args.Watch == null)
                throw new NullReferenceException("Watch ins't exists");

            else if (!args.Seller.Assortment.Contains(args.Watch.Brand))
                throw new ArgumentException($"There is no watches in {args.Seller.Name}");

            else if (args.Seller is null || args.Buyer is null)
                throw new ArgumentException("Buyer OR Seller isn't exists");

            else if (args.Buyer.Equals(args.Seller))
                throw new ArgumentException("Buyer is Seller");

            else if (args.Buyer.Money < args.TotalCost)
                throw new ArgumentException($"Not enough money at the {args.Buyer.Name} shop");

            else if (args.Seller.Assortment[args.Watch.Brand]?.Amount < args.Amount)
                throw new ArgumentException($"Not enough watches at the {args.Seller.Name} shop");

            else if (args.Amount <= 0)
                throw new ArgumentException("Amount of watches was <= 0");

            else Exchange(sender, args);
        }

        private static void Exchange(object sender, ExchangeEventArgs args) // Метод для подписывания на прослушивание события 
        {
            OnExchange?.Invoke(sender, args);
        }

        public static void Subsribe(ExchangeEventHandler m) 
        {
            OnExchange += m;
        }
    }
}
