using System;

namespace Task_A_2
{
    class Program
    {
        static void Main()
        {

            ComplexArrayList coll = new ComplexArrayList() 
            { 
                new ComplexNum(4, 7),
                new ComplexNum(-2, 24),
                new ComplexNum(6, 1),
                new ComplexNum(-4, -4),
                new ComplexNum(5, -10),
                new ComplexNum(5, 4)
            };

            coll.Add(4, 32);
            coll.Remove(4, 32);
            coll.Sort();

            foreach(var item in coll)
            {
                Console.WriteLine(item);
            }
            ComplexNum num = new ComplexNum(-2, 7);
            Console.WriteLine(num);
            Console.WriteLine("Closest:\t" + coll.FindClosest(num));

        }
    }
}
