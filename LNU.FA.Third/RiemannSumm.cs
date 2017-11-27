using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.FA.Third
{
    public class RiemannSumm
    {
        private readonly Func<double, double, double> function;
        private readonly int n;

        public RiemannSumm(Func<double, double, double> function, int n)
        {
            this.function = function;
            this.n = n;
        }

        public double CalculateRight(double t, double a, double b, double[] array)
        {
            Func<double, int, double> localFunc = (el, index) => function(t, el) * array[index];
            double result = default(double);
            double step = (a + b) / (double)n;

            var i = 0;
            for (double x = a; x < b; x += step)
            {
                result += localFunc(x + step, i) * step;
                i++;
            }

            return result;
        }
    }
}
