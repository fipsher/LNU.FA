using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.FA.Fourth
{
    class Program
    {

        static void Main(string[] args)
        {
            Func<double, double> func = (x) => x * x;
            double a = 0;
            double b = 10;
            var n = 10;
            var step = (b - a) / n;
            Point[] points = new Point[n];
            for (int i = 0; i < n; i++)
            {
                points[i] = new Point() { X = a + step * i, Y = func(a + step * i) };
            }


            Solver solverInstance = new Solver();
            solverInstance.Solve(points, 2);
            var result = solverInstance.Run(2);
        }
    }
}
