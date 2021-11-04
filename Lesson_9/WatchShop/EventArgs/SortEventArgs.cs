using System;

namespace WatchShop
{
    public class SortEventArgs : EventArgs
    {
        public Comparison<Watch> Comparison
        {
            get;
            set;
        }
    }
}
