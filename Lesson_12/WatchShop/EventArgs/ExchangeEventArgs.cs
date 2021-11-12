using System;

namespace WatchShop.Args
{
    // Класс аргументов для обмена часами между магазинами
    public class ExchangeEventArgs : EventArgs
    {
        public readonly Shop Seller;
        public readonly Shop Buyer;
        public readonly Watch Watch;
        public readonly int Amount;
        public decimal? TotalCost => Watch is null ? null : Amount * Watch.Cost;

        public ExchangeEventArgs(Shop seller, Shop buyer, Watch watch, int amount)
        {
            Seller = seller;
            Buyer = buyer;
            Watch = watch;
            Amount = amount;
        }
    }
}
