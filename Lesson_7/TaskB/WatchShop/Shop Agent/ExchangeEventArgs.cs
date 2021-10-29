using System;

namespace WatchShop
{
    public class ExchangeEventArgs : EventArgs
    {
        public Shop Seller;
        public Shop Buyer;
        public Watch Watch;
        public int Amount;

        public decimal? TotalCost => Watch is null ? null : Amount * Watch.Cost;
    }
}
