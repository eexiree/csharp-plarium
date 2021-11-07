using System;

namespace WatchShop.Args
{
    public class SortEventArgs : EventArgs
    {
        public readonly Comparison<Watch> Comparison;

        public SortEventArgs(Comparison<Watch> comparison)
        {
            Comparison = comparison;
        }
    }
}
