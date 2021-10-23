using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_A_2
{
    public struct ComplexNum
    {
        public readonly int real;
        public readonly int imgn;

        public double Modulus => Math.Sqrt(Math.Pow(real, 2.0) + Math.Pow(imgn, 2.0));

        public ComplexNum(int r, int i)
        {
            real = r;
            imgn = i;
        }

        public override string ToString()
        {
            return $"z={real}{imgn.ToString("+#;-#;0")}i\t{Modulus}";
        }
    }
}
