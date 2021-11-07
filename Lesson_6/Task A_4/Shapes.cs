using System;

namespace Task_A_4
{
    public class Cell : IComparable
    {
        // [0] - top left
        // [1] - top right
        // [2] - bottom left
        // [3] - bottom right
        public readonly Point[] vertices = new Point[4];
        public uint CellID
        {
            get;
            private set;
        }
        private static uint CellCounter;

        // Creates a cell by passing top left point coords
        public Cell(int x, int y)
        {
            vertices[0] = new Point(x, y);
            vertices[1] = new Point(x + 1, y);
            vertices[2] = new Point(x, y - 1);
            vertices[3] = new Point(x + 1, y - 1);
            CellID = ++CellCounter;
        }

        public double Distance(Point other)
        {
            double x = (double)vertices[0].x - 0.5;
            double y = (double)vertices[0].y - 0.5;
            return Math.Sqrt(Math.Pow(x - (double)other.x, 2.0) + Math.Pow(y - (double)other.y, 2.0));
        }

        public override string ToString()
        {
            int padding = 15;
            return $"Cell {CellID}:\n{"Top Left:".PadLeft(padding, '.')}\t{vertices[0]}\n{"Top Right:".PadLeft(padding, '.')}\t{vertices[1]}\n{"Bottom Left:".PadLeft(padding, '.')}\t{vertices[2]}\n{"Bottom Right:".PadLeft(padding, '.')}\t{vertices[3]}";
        }

        public int CompareTo(object obj)
        {
            Cell other = obj as Cell;

        }
    }

    public class Circle
    {
        public Point Center
        {
            get;
            private set;
        }
        public double Radius
        {
            get;
            private set;
        }

        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

    }

    public struct Point
    {
        public readonly int x;
        public readonly int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public double Distance(Point other) => Math.Sqrt(Math.Pow(x - other.x, 2.0) + Math.Pow(y - other.y, 2.0));
        public override string ToString()
        {
            return $"X: {x}\tY: {y}";
        }
    }
}
