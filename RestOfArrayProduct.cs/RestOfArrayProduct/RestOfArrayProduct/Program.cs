using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestOfArrayProduct
{
    public class Program
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

        static void Main(string[] args)
        {
            int[] values = new int[] { 1, 2, 3, 4};
            Program program = new Program();
            int [] result = program.SkipIndexProductV1(values);
            Console.Read();
            
        }

        public int[] SkipIndexProductV1(int[] input)
        {
            stopWatch.Start();
            int[] returnValue = new int[input.Length];

            for(int i = 0; i < input.Length; i++)
            {
                returnValue[i] = 1;
                for (int j = 0; j < input.Length; j++)
                {
                    if(i == j)
                    {
                        continue;
                    }

                    returnValue[i] = returnValue[i] * input[j] ;
                    continue;
                }
            }

            stopWatch.Stop();
            System.Diagnostics.Trace.WriteLine("Run time V1: " + stopWatch.Elapsed);
            stopWatch.Reset();
            return returnValue;
        }

        public int[] SkipIndexProductV2(int[] input)
        {
            stopWatch.Start();
            int[] returnValue = new int[input.Length];
            int total = 1;

            for (int i = 0; i < input.Length; i++)
            {
                total = total * input[i];
            }

            int returnDivider = 1;

            for (int i = 0; i < input.Length; i++)
            {
                returnValue[i] = total / returnDivider;
                returnDivider++;
            }

            stopWatch.Stop();
            System.Diagnostics.Trace.WriteLine("Run time V2: " + stopWatch.Elapsed);
            stopWatch.Reset();
            return returnValue;
        }

        public int[] SkipIndexProductV3(int[] a)
        {
            stopWatch.Start();
            int N = a.Length;
            int[] products = new int[N];

            // Get the products below the current index
            int p = 1;
            for (int i = 0; i < N; ++i)
            {
                products[i] = p;
                p *= a[i];
            }

            // Get the products above the curent index
            p = 1;
            for (int i = N - 1; i >= 0; --i)
            {
                products[i] *= p;
                p *= a[i];
            }
            stopWatch.Stop();
            System.Diagnostics.Trace.WriteLine("Run time V3: " + stopWatch.Elapsed);
            stopWatch.Reset();
            return products;
        }
    }
}
