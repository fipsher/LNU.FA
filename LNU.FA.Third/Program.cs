using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.FA.Third
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, double, double> k = (t, s) => t * s;
            Func<double, double> y = (el) => 1;
            double lambda = 1;

            FredgolmSolver solver = new FredgolmSolver(lambda, k, y);

            var a = solver.Calculate(1, 0, 1);
            Console.WriteLine(a);
        }
    }
}
