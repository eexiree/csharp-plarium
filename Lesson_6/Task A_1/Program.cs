using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Task_A_1
{
    class EntryPoint
    {
        static void Main()
        {
            int n = 100000;
            var elapsedTimeArrayList = ArrayListTest(n);
            var elapsedTimeLinkedList = LinkedListTest(n);
            Console.WriteLine($"ArrayList spent time:\t{elapsedTimeArrayList} ticks\n\nLinkedList spent time:\t{elapsedTimeLinkedList} ticks");
        }

        private static long ArrayListTest(int n)
        {
            ArrayList arrLst = new ArrayList(n);
            Stopwatch sw = new Stopwatch();
            for (int j = 1; j <= n; j++)
            {
                arrLst.Add(j);
            }
            sw.Start();
            int i = 1;
            while (arrLst.Count > 1)
            {
                for (; i < arrLst.Count; i += 2)
                {
                    arrLst.RemoveAt(i);
                    i--;
                }
                i = (n & 1) == 1 ? 0 : 1;
            }
            sw.Stop();
            return sw.ElapsedTicks;
        }

        private static long LinkedListTest(int n)
        {
            LinkedList<int> lnkLst = new LinkedList<int>();
            Stopwatch sw = new Stopwatch();
            for (int i = n; i > 0; i--)
            {
                lnkLst.AddFirst(i);
            }
            LinkedListNode<int> node = lnkLst.First.Next;
            int nodeValue = 0;
            sw.Start();
            while (lnkLst.Count > 1)
            {
                while (node != null)
                {
                    nodeValue = node.Value;
                    node = node.Next ?? null;
                    node = node == null ? null : node.Next;
                    lnkLst.Remove(nodeValue);
                }
                node = (n & 1) == 1 ? lnkLst.First : lnkLst.First.Next ?? lnkLst.First;
            }
            sw.Stop();
            return sw.ElapsedTicks;
        }
    }
}
