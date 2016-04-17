using System;

namespace PrimeTable
{
    class Program
    {
        static void Main()
        {
            Console.Write("Type table size or Enter for default table size of 10: ");
            ulong tableSize;
            bool validuLong = ulong.TryParse(Console.ReadLine(), out tableSize);

            if (validuLong == false)
            {
                tableSize = 10;
                validuLong = true;
            }

            while (!validuLong)
            {
                Console.WriteLine("The value is not a valid number.");
                Console.Write("Please enter a number 2 or greater. ");

                validuLong = ulong.TryParse(Console.ReadLine(), out tableSize);
            }

            GenerateTable table = new GenerateTable();

            table.PrintTable(tableSize);

            Console.Read();
        }
    }
}
