using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDrecursion
{
    public class LDR
    {
        public void coeff_udpate(short[] a_L, short[] a_R, short k, out short[] b)
        {
            b = new short[11];
            b[0] = (short)(((a_L[0] << 15) + (k * a_R[0]) + 0x4000) >> 15);
            b[1] = (short)(((a_L[1] << 15) + (k * a_R[1]) + 0x4000) >> 15);
            b[2] = (short)(((a_L[2] << 15) + (k * a_R[2]) + 0x4000) >> 15);
            b[3] = (short)(((a_L[3] << 15) + (k * a_R[3]) + 0x4000) >> 15);
            b[4] = (short)(((a_L[4] << 15) + (k * a_R[4]) + 0x4000) >> 15);
            b[5] = (short)(((a_L[5] << 15) + (k * a_R[5]) + 0x4000) >> 15);
            b[6] = (short)(((a_L[6] << 15) + (k * a_R[6]) + 0x4000) >> 15);
            b[7] = (short)(((a_L[7] << 15) + (k * a_R[7]) + 0x4000) >> 15);
            b[8] = (short)(((a_L[8] << 15) + (k * a_R[8]) + 0x4000) >> 15);
            b[9] = (short)(((a_L[9] << 15) + (k * a_R[9]) + 0x4000) >> 15);
            b[10] = (short)(((a_L[10] << 15) + (k * a_R[10]) + 0x4000) >> 15);
        }

        public void reflect_coeff(short k_tmp, out short k, out short b)
        {
            //k = (short)(-Rn / ((Rd + 0x4000) >> 15));
            k = (short)(((k_tmp * 0x7ff8) + 0x4000) >> 15);
            b = (short)((k_tmp + 0x2) >> 2);
        }

        public void NumDen(short[] R_num, short[] R_den, short[] a, out int Rn, out int Rd)
        {
            Rn = R_num[0] * a[0] + R_num[1] * a[1] + R_num[2] * a[2] + R_num[3] * a[3] + R_num[4] * a[4] + R_num[5] * a[5] + R_num[6] * a[6] + R_num[7] * a[7] + R_num[8] * a[8] + R_num[9] * a[9] + R_num[10] * a[10];  
            Rd = R_den[0] * a[0] + R_den[1] * a[1] + R_den[2] * a[2] + R_den[3] * a[3] + R_den[4] * a[4] + R_den[5] * a[5] + R_den[6] * a[6] + R_den[7] * a[7] + R_den[8] * a[8] + R_den[9] * a[9] + R_den[10] * a[10];
        }

        public short[] LDRfixedpt(short[] R)
        {
            short[] a = new short[11];
            short[] a1 = new short[11];
            short[] a2 = new short[11];
            short[] a3 = new short[11];
            short[] a4 = new short[11];
            short[] a5 = new short[11];
            short[] a6 = new short[11];
            short[] a7 = new short[11];
            short[] a8 = new short[11];
            short[] a9 = new short[11];
            short[] a10 = new short[11];

            short[] a_r = new short[11];
            short[] a1_r = new short[11];
            short[] a2_r = new short[11];
            short[] a3_r = new short[11];
            short[] a4_r = new short[11];
            short[] a5_r = new short[11];
            short[] a6_r = new short[11];
            short[] a7_r = new short[11];
            short[] a8_r = new short[11];
            short[] a9_r = new short[11];
            short[] a10_r = new short[11];

            short k = 0;
            short b = 0;  // temporary vector to store prediction coefficients
            short[] R_num = new short[11];
            short[] R_den = new short[11];
            int Rn; // inner loop numerator
            int Rd; // inner loop denominator
            short k_tmp;

            a[0] = 8192; // Initialize first coefficient

            // Iteration 1
            R_den[0] = R[0];
            R_den[1] = R[1];

            R_num[0] = R[1];
            R_num[1] = R[0];
            NumDen(R_num, R_den, a, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            coeff_udpate(a, a_r, k, out a1);
            a1[1] = b;

            // Iteration 2
            R_den[0] = R[0];
            R_den[1] = R[1];
            R_den[2] = R[2];

            R_num[0] = R[2];
            R_num[1] = R[1];
            R_num[2] = R[0];
            NumDen(R_num, R_den, a1, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            a1_r[1] = a1[1];
            coeff_udpate(a1, a1_r, k, out a2);
            a2[2] = b;

            // Iteration 3
            R_den[0] = R[0];
            R_den[1] = R[1];
            R_den[2] = R[2];
            R_den[3] = R[3];

            R_num[0] = R[3];
            R_num[1] = R[2];
            R_num[2] = R[1];
            R_num[3] = R[0];
            NumDen(R_num, R_den, a2, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            a2_r[1] = a2[2];
            a2_r[2] = a2[1];
            coeff_udpate(a2, a2_r, k, out a3);
            a3[3] = b;

            // Iteration 4
            R_den[0] = R[0];
            R_den[1] = R[1];
            R_den[2] = R[2];
            R_den[3] = R[3];
            R_den[4] = R[4];

            R_num[0] = R[4];
            R_num[1] = R[3];
            R_num[2] = R[2];
            R_num[3] = R[1];
            R_num[4] = R[0];
            NumDen(R_num, R_den, a3, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            a3_r[1] = a3[3];
            a3_r[2] = a3[2];
            a3_r[3] = a3[1];
            coeff_udpate(a3, a3_r, k, out a4);
            a4[4] = b;

            // Iteration 5
            R_den[0] = R[0];
            R_den[1] = R[1];
            R_den[2] = R[2];
            R_den[3] = R[3];
            R_den[4] = R[4];
            R_den[5] = R[5];

            R_num[0] = R[5];
            R_num[1] = R[4];
            R_num[2] = R[3];
            R_num[3] = R[2];
            R_num[4] = R[1];
            R_num[5] = R[0];
            NumDen(R_num, R_den, a4, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            a4_r[1] = a4[4];
            a4_r[2] = a4[3];
            a4_r[3] = a4[2];
            a4_r[4] = a4[1];
            coeff_udpate(a4, a4_r, k, out a5);
            a5[5] = b;

            // Iteration 6
            R_den[0] = R[0];
            R_den[1] = R[1];
            R_den[2] = R[2];
            R_den[3] = R[3];
            R_den[4] = R[4];
            R_den[5] = R[5];
            R_den[6] = R[6];

            R_num[0] = R[6];
            R_num[1] = R[5];
            R_num[2] = R[4];
            R_num[3] = R[3];
            R_num[4] = R[2];
            R_num[5] = R[1];
            R_num[6] = R[0];
            NumDen(R_num, R_den, a5, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            a5_r[1] = a5[5];
            a5_r[2] = a5[4];
            a5_r[3] = a5[3];
            a5_r[4] = a5[2];
            a5_r[5] = a5[1];
            coeff_udpate(a5, a5_r, k, out a6);
            a6[6] = b;

            // Iteration 7
            R_den[0] = R[0];
            R_den[1] = R[1];
            R_den[2] = R[2];
            R_den[3] = R[3];
            R_den[4] = R[4];
            R_den[5] = R[5];
            R_den[6] = R[6];
            R_den[7] = R[7];

            R_num[0] = R[7];
            R_num[1] = R[6];
            R_num[2] = R[5];
            R_num[3] = R[4];
            R_num[4] = R[3];
            R_num[5] = R[2];
            R_num[6] = R[1];
            R_num[7] = R[0];
            NumDen(R_num, R_den, a6, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            a6_r[1] = a6[6];
            a6_r[2] = a6[5];
            a6_r[3] = a6[4];
            a6_r[4] = a6[3];
            a6_r[5] = a6[2];
            a6_r[6] = a6[1];
            coeff_udpate(a6, a6_r, k, out a7);
            a7[7] = b;

            // Iteration 8
            R_den[0] = R[0];
            R_den[1] = R[1];
            R_den[2] = R[2];
            R_den[3] = R[3];
            R_den[4] = R[4];
            R_den[5] = R[5];
            R_den[6] = R[6];
            R_den[7] = R[7];
            R_den[8] = R[8];

            R_num[0] = R[8];
            R_num[1] = R[7];
            R_num[2] = R[6];
            R_num[3] = R[5];
            R_num[4] = R[4];
            R_num[5] = R[3];
            R_num[6] = R[2];
            R_num[7] = R[1];
            R_num[8] = R[0];
            NumDen(R_num, R_den, a7, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            a7_r[1] = a7[7];
            a7_r[2] = a7[6];
            a7_r[3] = a7[5];
            a7_r[4] = a7[4];
            a7_r[5] = a7[3];
            a7_r[6] = a7[2];
            a7_r[7] = a7[1];
            coeff_udpate(a7, a7_r, k, out a8);
            a8[8] = b;

            // Iteration 9
            R_den[0] = R[0];
            R_den[1] = R[1];
            R_den[2] = R[2];
            R_den[3] = R[3];
            R_den[4] = R[4];
            R_den[5] = R[5];
            R_den[6] = R[6];
            R_den[7] = R[7];
            R_den[8] = R[8];
            R_den[9] = R[9];

            R_num[0] = R[9];
            R_num[1] = R[8];
            R_num[2] = R[7];
            R_num[3] = R[6];
            R_num[4] = R[5];
            R_num[5] = R[4];
            R_num[6] = R[3];
            R_num[7] = R[2];
            R_num[8] = R[1];
            R_num[9] = R[9];
            NumDen(R_num, R_den, a8, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            a8_r[1] = a8[8];
            a8_r[2] = a8[7];
            a8_r[3] = a8[6];
            a8_r[4] = a8[5];
            a8_r[5] = a8[4];
            a8_r[6] = a8[3];
            a8_r[7] = a8[2];
            a8_r[8] = a8[1];
            coeff_udpate(a8, a8_r, k, out a9);
            a9[9] = b;

            // Iteration 10
            R_den[0] = R[0];
            R_den[1] = R[1];
            R_den[2] = R[2];
            R_den[3] = R[3];
            R_den[4] = R[4];
            R_den[5] = R[5];
            R_den[6] = R[6];
            R_den[7] = R[7];
            R_den[8] = R[8];
            R_den[9] = R[9];
            R_den[10] = R[10];

            R_num[0] = R[10];
            R_num[1] = R[9];
            R_num[2] = R[8];
            R_num[3] = R[7];
            R_num[4] = R[6];
            R_num[5] = R[5];
            R_num[6] = R[4];
            R_num[7] = R[3];
            R_num[8] = R[2];
            R_num[9] = R[1];
            R_num[10] = R[0];
            NumDen(R_num, R_den, a9, out Rn, out Rd);

            k_tmp = (short)(-Rn / ((Rd + 0x4000) >> 15));

            reflect_coeff(k_tmp, out k, out b);

            a9_r[1] = a9[9];
            a9_r[2] = a9[8];
            a9_r[3] = a9[7];
            a9_r[4] = a9[6];
            a9_r[5] = a9[5];
            a9_r[6] = a9[4];
            a9_r[7] = a9[3];
            a9_r[8] = a9[2];
            a9_r[9] = a9[1];
            coeff_udpate(a9, a9_r, k, out a10);
            a10[10] = b;

            return a10;
        }
    }
}
