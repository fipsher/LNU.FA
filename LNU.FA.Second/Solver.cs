using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.FA.Second
{
    public static class Solver
    {
        private static double[,] A;
        private static double[] Lc;

        private static double[,] TransformToA(double[,] bMatrix, double lambda)
        {
            var resultMAtrix = new double[bMatrix.GetLength(0), bMatrix.GetLength(1)];

            for (int i = 0; i < bMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < bMatrix.GetLength(0); j++)
                {
                    resultMAtrix[i, j] = i == j
                        ? 1 - lambda * bMatrix[i, j]
                        : -lambda * bMatrix[i, j];
                }
            }

            return resultMAtrix;
        }

        private static double Rho(double[] x1, double[] x2)
        {
            double max = 0;

            for (int j = 0; j < x1.Length; j++)
            {
                double sum = Math.Abs(x1[j] - x2[j]);
                max = max > sum ? max : sum;
            }
            return max;
        }

        private static double[] Iterate(double[] x)
        {
            double[] tempResult = A.Multiply(x);

            return tempResult.Plus(Lc);
        }

        private static double GetLambda(double[,] matrix)
        {
            List<double> minDList = new List<double>();
            List<double> maxDList = new List<double>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var mind = matrix[i, i];
                var maxd = matrix[i, i];
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        mind -= Math.Abs(matrix[i, j]);
                        maxd += Math.Abs(matrix[i, j]);
                    }
                }
                minDList.Add(mind);
                maxDList.Add(maxd);
            }
            Console.WriteLine($"d={minDList.Min()};   D={maxDList.Max()}");
            return 2d / (minDList.Min() + maxDList.Max());
        }

        public static void FixInputs(ref double[,] b, ref double[] c)
        {
            for (int i = 0; i < b.GetLength(0); i++)
            {
                if (b[i, i] < 0)
                {
                    for (int j = 0; j < b.GetLength(1); j++)
                    {
                        b[i, j] *= -1;
                    }
                    c[i] *= -1;
                }
            }
        }

        public static List<double[]> Solve(double[,] b, double[] c, double[] x0, double error)
        {
            FixInputs(ref b, ref c);
            double lambda = GetLambda(b);
            A = TransformToA(b, lambda);
            Lc = c.Select(el => el * lambda).ToArray();

            double[] x1 = x0.GetCopy();
            double[] x2 = Iterate(x1);
             
            var result = new List<double[]>
            {
                x1.GetCopy(),
                x2.GetCopy()
            };

            while (Rho(x1, x2) > error)
            {
                x1 = x2.GetCopy();
                x2 = Iterate(x1);

                result.Add(x2.GetCopy());
            }

            return result;
        }
    }
}
