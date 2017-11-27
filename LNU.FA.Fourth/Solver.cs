using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.FA.Fourth
{
    public class Solver
    {
        private int N;
        private double H;

        private double BaseFunc(double x, int index)
        {
            return Math.Pow(x, index);
        }

        private double Sacalar1(double[] y, double[] g)
        {
            var tempList = y.Zip(g, (a, b) => a * b);
            return Intergal(tempList.ToArray());
        }

        private double[] weights;

        public void Solve(Point[] points, int degree)
        {
            degree++;
            N = points.Length;
            H = points[1].X - points[0].X;
            Matrix matrix = new DenseMatrix(points.Length, degree);
            Vector vector = new DenseVector(points.Length);

            for (int i = 0; i < points.Length; i++)
            {
                for (int j = 0; j < degree; j++)
                {
                    var array = points.Select(el => BaseFunc(el.X, i) * BaseFunc(el.X, j)).ToArray();

                    matrix[i, j] = Intergal(array);
                }
                var yArray = points.Select(el => BaseFunc(el.X, i) * el.Y).ToArray();
                vector[i] = Intergal(yArray);
            }
            weights = matrix.Solve(vector).ToArray();
        }

        public double Run(double x)
        {
            double result = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                result += BaseFunc(x, i) * weights[i];
            }
            return result;
        }

        private double Intergal(double[] array)
        {
            return array.Sum(e => e * H);
        }
    }
}