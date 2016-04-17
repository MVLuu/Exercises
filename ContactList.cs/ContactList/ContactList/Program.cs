using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test with a non-empty list of integers.
            GenericList<int> gll = new GenericList<int>();
            gll.AddNode(5);
            gll.AddNode(4);
            gll.AddNode(3);
            int intVal = gll.GetLast();
            // The following line displays 5.
            System.Console.WriteLine(intVal);

            // Test with an empty list of integers.
            GenericList<int> gll2 = new GenericList<int>();
            intVal = gll2.GetLast();
            // The following line displays 0.
            System.Console.WriteLine(intVal);

            // Test with a non-empty list of strings.
            GenericList<string> gll3 = new GenericList<string>();
            gll3.AddNode("five");
            gll3.AddNode("four");
            string sVal = gll3.GetLast();
            // The following line displays five.
            System.Console.WriteLine(sVal);

            // Test with an empty list of strings.
            GenericList<string> gll4 = new GenericList<string>();
            sVal = gll4.GetLast();
            // The following line displays a blank line.
            System.Console.WriteLine(sVal);
        }
    }
}
