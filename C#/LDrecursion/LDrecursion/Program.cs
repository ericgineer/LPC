using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDrecursion
{
    // Eric wilson -- 4/26/15
    // This class contains two fuctions, LDRfloat and LDRfixed.
    // Both perform Levinson-Durbin recursion in an unwrapped form
    // designed to simulate implementation in an FPGA
    class LevinsonDurbin
    {
        static double[] LDRfloat(double[] R)
        {
            double[] a = new double[10];
            double[] a1 = new double[10];
            double[] a2 = new double[10];
            double[] a3 = new double[10];
            double[] a4 = new double[10];
            double[] a5 = new double[10];
            double[] a6 = new double[10];
            double[] a7 = new double[10];
            double[] a8 = new double[10];
            
            double   Ek = 0;
            double   lambda = 0;

            a8[0] = 1;

            // Iteration 1
            a[1] = -R[1] / R[0];
            Ek   = R[0] + R[1] * a[1];

            lambda = -(R[2] + a[1] * R[1]) / Ek;
            a1[1]   = a[1] + lambda * a[1];
            a1[2]   = lambda;
            Ek     = (1 - lambda * lambda) * Ek;

            // Iteration 2
            lambda = -(R[3] + a1[1] * R[2] + a1[2] * R[1]) / Ek;
            a2[1] = a1[1] + lambda * a1[2];
            a2[2] = a1[2] + lambda * a1[1];
            a2[3] = lambda;
            Ek   = (1 - lambda * lambda) * Ek;

            // Iteration 3
            lambda = -(R[4] + a2[1] * R[3] + a2[2] * R[2] + a2[3] * R[1]) / Ek;
            a3[1] = a2[1] + lambda * a2[3];
            a3[2] = a2[2] + lambda * a2[2];
            a3[3] = a2[3] + lambda * a2[1];
            a3[4] = lambda;
            Ek = (1 - lambda * lambda) * Ek;

            // Iteration 4
            lambda = -(R[5] + a3[1] * R[4] + a3[2] * R[3] + a3[3] * R[2] + a3[4] * R[1]) / Ek;
            a4[1] = a3[1] + lambda * a3[4];
            a4[2] = a3[2] + lambda * a3[3];
            a4[3] = a3[3] + lambda * a3[2];
            a4[4] = a3[4] + lambda * a3[1];
            a4[5] = lambda;
            Ek = (1 - lambda * lambda) * Ek;

            // Iteration 5
            lambda = -(R[6] + a4[1] * R[5] + a4[2] * R[4] + a4[3] * R[3] + a4[4] * R[2] + a4[5] * R[1]) / Ek;
            a5[1] = a4[1] + lambda * a4[5];
            a5[2] = a4[2] + lambda * a4[4];
            a5[3] = a4[3] + lambda * a4[3];
            a5[4] = a4[4] + lambda * a4[2];
            a5[5] = a4[5] + lambda * a4[1];
            a5[6] = lambda;
            Ek = (1 - lambda * lambda) * Ek;

            // Iteration 6
            lambda = -(R[7] + a5[1] * R[6] + a5[2] * R[5] + a5[3] * R[4] + a5[4] * R[3] + a5[5] * R[2] + a5[6] * R[1]) / Ek;
            a6[1] = a5[1] + lambda * a5[6];
            a6[2] = a5[2] + lambda * a5[5];
            a6[3] = a5[3] + lambda * a5[4];
            a6[4] = a5[4] + lambda * a5[3];
            a6[5] = a5[5] + lambda * a5[2];
            a6[6] = a5[6] + lambda * a5[1];
            a6[7] = lambda;
            Ek = (1 - lambda * lambda) * Ek;

            // Iteration 7
            lambda = -(R[8] + a6[1] * R[7] + a6[2] * R[6] + a6[3] * R[5] + a6[4] * R[4] + a6[5] * R[3] + a6[6] * R[2] + a6[7] * R[1]) / Ek;
            a7[1] = a6[1] + lambda * a6[7];
            a7[2] = a6[2] + lambda * a6[6];
            a7[3] = a6[3] + lambda * a6[5];
            a7[4] = a6[4] + lambda * a6[4];
            a7[5] = a6[5] + lambda * a6[3];
            a7[6] = a6[6] + lambda * a6[2];
            a7[7] = a6[7] + lambda * a6[1];
            a7[8] = lambda;
            Ek = (1 - lambda * lambda) * Ek;

            // Iteration 8
            lambda = -(R[9] + a7[1] * R[8] + a7[2] * R[7] + a7[3] * R[6] + a7[4] * R[5] + a7[5] * R[4] + a7[6] * R[3] + a7[7] * R[2] + a7[8] * R[1]) / Ek;
            a8[1] = a7[1] + lambda * a7[8];
            a8[2] = a7[2] + lambda * a7[7];
            a8[3] = a7[3] + lambda * a7[6];
            a8[4] = a7[4] + lambda * a7[5];
            a8[5] = a7[5] + lambda * a7[4];
            a8[6] = a7[6] + lambda * a7[3];
            a8[7] = a7[7] + lambda * a7[2];
            a8[8] = a7[8] + lambda * a7[1];
            a8[9] = lambda;

            return a8;
        }

        static short[] Levinson_Durbin(short[] R, int p)
        {
            short[] a = new short[p+1];
            short[] k = new short[p+1];
            int i, m;  // inner and outer loop index
            short[] b = new short[p+1];  // temporary vector to store prediction coefficients
            int Rn, Rd; // inner loop numerator, inner loop denominator

            short    shift = 15; // Number of bits to shift right
            short round = Convert.ToInt16(Math.Pow(2, shift-1));  // amount to add to the value being shifted for rounding
            short scale = 32760; // 32760

            a[0] = 8192; // initialize a[0] to 1/4

            // For each order, calculate a new prediction and reflection coefficient
            for (m = 1; m < p + 1; m++)
            {
                // Initialize the numerator and denominator accumulators to zero
                Rn = Rd = 0;

                // Calculate the numerator and denominator values for the integer division
                for (i = 0; i < m; i++)
                {
                    Rn = Rn + (R[m - i] * a[i]);
                    Rd = Rd + (R[i] * a[i]);
                }

                // Calculate the reflection coefficient k[m]. Round the Q28 number prior
                // to converting it to a Q15 number. Also, scale it by the scaling factor
                // to help keep the input data in proper range.
                k[m] = (short)(-Rn / ((Rd + round) >> shift));
                k[m] = (short)(((k[m] * scale) + round) >> shift);

                // Calculate the new prediction coefficient by converting k[m] from
                // a Q15 to a Q13 number
                b[m] = (short)((k[m] + 0x2) >> 2);
               
                // Calculate the new prediction coefficients for the next iteration
                for (i = 1; i < m; i++)
                    b[i] = (short)(((a[i] << shift) + (k[m] * a[m - i]) + round) >> shift);
                
                // Copy the prediction coefficients from the temporary b[] array to a[]
                for ( i = 1; i < m + 1; i++)
                    a[i] = b[i];
            }
            return a;
        } // end Levinson_Durbin()

        static void Main(string[] args)
        {
            LDR levdur = new LDR();

            short[] a = new short[10];
            short[] a2 = new short[10];
            short[] R = { 32767, 25742, 16169, 9836, 4569, -2674, -11249, -17338, -14853, -6828, -3174 };

            a = Levinson_Durbin(R, 10);
            a2 = levdur.LDRfixedpt(R);

            Console.WriteLine("\n\nLDR fixed point coefficients: \n");
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine("{0}", a[i]);
            }

            Console.WriteLine("\n\nLDR unwrapped fixed point coefficients: \n");
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine("{0}", a2[i]);
            }

            Console.WriteLine("\n\nPress any key to EXIT!\n");
            Console.Read();
        }
    }
}
