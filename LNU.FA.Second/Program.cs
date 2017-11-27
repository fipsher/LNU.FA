using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.FA.Second
{
    class Program
    {
        private static double[] _vector = { 3, 5, 7 };

        private static double[,] _matrix =
        {
            { 8, -4, 1 },
            { 1, 7, 3 },
            { 3, -1, -5 }
        };

        static void Main(string[] args)
        {
            var error = 0.0001;
            double[] initVector = Enumerable.Repeat(0d, _vector.Length).ToArray();

            List<double[]> result = Solver.Solve(_matrix, _vector, initVector, error);

            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine($"Step[{i}]: [{string.Join(", ", result[i])}]");
            }

            Console.ReadKey();
        }
    }
}
