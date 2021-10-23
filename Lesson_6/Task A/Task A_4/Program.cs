using System;
using System.Collections.Generic;

namespace Task_A_4
{
    class Program
    {
        static void Main()
        {
            CellList cellLst = new CellList();
            Circle circle = new Circle(new Point(0, 0), 3.0);
            cellLst.Fill(20, 6);
            Console.WriteLine(cellLst);
            List<Cell> cellsInCirle = cellLst.InCircleRange(circle);
            foreach(var cell in cellsInCirle)
            {
                Console.WriteLine(cell);
            }
        }
    }
}
