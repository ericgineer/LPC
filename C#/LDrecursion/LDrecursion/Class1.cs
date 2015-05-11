using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDrecursion
{
    class LDRfixedpt
    {


        private short[] LDRfixedpt(short[] R, int p)
        {
            short[] a = new short[p + 1];
            short[] k = new short[p + 1];
            int i, m;  // inner and outer loop index
            short[] b = new short[p + 1];  // temporary vector to store prediction coefficients
            int Rn, Rd; // inner loop numerator, inner loop denominator

            short shift = 15; // Number of bits to shift right
            short round = Convert.ToInt16(Math.Pow(2, shift - 1));  // amount to add to the value being shifted for rounding
            short scale = 32760; // 32760
            return a;
        }
    }
}
