using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task_A_4
{
    public class CellList : IEnumerable
    {
        private readonly List<Cell> _cells = new List<Cell>();

        public void Add(Cell cell)
        {
            _cells.Add(cell);
        }
        public void Remove(Cell cell)
        {
            _cells.Remove(cell);
        }
        public void RemoveAt(int index)
        {
            _cells.RemoveAt(index);
        }
        public void Fill(int amount, int range)
        {
            Random rng = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < amount; i++)
            {
                int x = rng.Next(-range, range + 1);
                int y = rng.Next(-range, range + 1);
                _cells.Add(new Cell(x, y));
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => _cells.GetEnumerator();

        public List<Cell> InCircleRange(Circle circle)
        {
            List<Cell> inCirle = new List<Cell>();
            foreach(var cell in _cells)
            {
                bool _in = true;
                foreach (var vert in cell.vertices)
                {     
                    if (vert.Distance(circle.Center) >= circle.Radius)
                    {
                        _in = false;
                        break;
                    }   
                }
                if (_in)
                    inCirle.Add(cell);
            }
            inCirle.Sort(Comparer<Cell>.Create((Cell a, Cell b) =>
            {
                if (a.Distance(circle.Center) > b.Distance(circle.Center))
                    return 1;
                else if (a.Distance(circle.Center) < b.Distance(circle.Center))
                    return -1;
                else
                    return 0;
            }));
            return inCirle;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach(var cell in _cells)
            {
                output.Append(cell.ToString() + "\n\n");
            }
            return output.ToString();
        }
    }
}
