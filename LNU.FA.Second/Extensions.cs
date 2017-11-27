using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.FA.Second
{
    public static class Extensions
    {
        public static double[] Multiply(this double[,] matrix, double[] vector)
        {
            double[] aux = new double[vector.Length];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                aux[i] = 0;
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    aux[i] += vector[j] * matrix[i, j];
                }
            }
            return aux;
        }

        public static double[] Plus(this double[] vector1, double[] vector2)
        {
            return vector1.Zip(vector2, (a,b) => a + b).ToArray();
        }

        public static double[] GetCopy(this double[] vector)
        {
            double[] result = new double[vector.Length];
            vector.CopyTo(result, 0);
            return result;
        }
    }
}
