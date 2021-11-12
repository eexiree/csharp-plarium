using System;

namespace WatchShop.Args
{
    // Класс аргументов для сортировки ассортимента магазина по разным критериям
    public class SortEventArgs : EventArgs
    {
        public readonly Comparison<Watch> Comparison;

        public SortEventArgs(Comparison<Watch> comparison)
        {
            Comparison = comparison;
        }
    }
}
