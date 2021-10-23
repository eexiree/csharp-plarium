using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_A_2
{
    public class ComplexArrayList : IEnumerable
    {
        private readonly ArrayList _numbers = new ArrayList();

        public int Count => _numbers.Count;
        public ComplexNum this[int index] => (ComplexNum)_numbers[index];

        public void Add(ComplexNum num)
        {
            _numbers.Add(num);
        }
        public void Add(int real, int imgn)
        {
            _numbers.Add(new ComplexNum(real, imgn));
        }
        public void Remove(ComplexNum num)
        {
            _numbers.Remove(num);
        }
        public void Remove(int real, int imgn)
        {
            _numbers.Remove(new ComplexNum(real, imgn));
        }
        public void RemoveAt(int index)
        {
            _numbers.RemoveAt(index);
        }
        public void Sort()
        {
            _numbers.Sort(Comparer<ComplexNum>.Create((z, w) =>
            {
                if (z.real < w.real)
                    return -1;
                else if (z.real == w.real && z.imgn < w.imgn)
                    return 1;
                else 
                    return 0;
            }
            ));
        }

        public ComplexNum FindClosest(ComplexNum other)
        {
            ComplexNum closest = (ComplexNum)_numbers[0];
            double closestModulus = Math.Abs(other.Modulus - closest.Modulus);
            foreach(ComplexNum num in _numbers)
            {
                if (Math.Abs(other.Modulus - num.Modulus) < closestModulus)
                {
                    closest = num;
                    closestModulus = Math.Abs(other.Modulus - closest.Modulus);
                }
            }
            return closest;
        }

        IEnumerator IEnumerable.GetEnumerator() => _numbers.GetEnumerator();
    }
}
