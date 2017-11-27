using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.FA.Third
{
    public class FredgolmSolver
    {
        public int N { get; set; } = 100;
        public double eps { get; set; } = 0.001;

        private readonly double lambda;
        private readonly Func<double, double, double> k;
        private readonly Func<double, double> y;

        public FredgolmSolver(double lambda, Func<double, double, double> k, Func<double, double> y)
        {
            this.lambda = lambda;
            this.k = k;
            this.y = y;
        }

        public double Calculate(double t, double a, double b, double[] initArray = null)
        {
            if (initArray == null || !initArray.Any())
            {
                initArray = Enumerable.Repeat(1d, N+1).ToArray();
            }



            double x = 0;
            RiemannSumm riemannSumm = new RiemannSumm(k, N);
            double integral = riemannSumm.CalculateRight(t, a, b, initArray);

            double nextX = y(t) + lambda * integral;
            do
            {
                x = nextX;
                var xArray = InternalCalculate(a, b, initArray);

                integral = riemannSumm.CalculateRight(t, a, b, xArray);
                nextX = y(t) + lambda * integral;

                initArray = xArray;
            }
            while (Math.Abs(x - nextX) > eps);

            return nextX;
        }

        private double[] InternalCalculate(double a, double b, double[] array = null)
        {
            double step = (a + b) / (double)N;
            double[] x_0 = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                var t = a + step * i;
                var riemannSumm = new RiemannSumm(k, N);
                var initIntegral = riemannSumm.CalculateRight(t, a, b, array);
                    
                x_0[i] = y(t) + lambda * initIntegral;
            }

            return x_0;
        }
    }
}
