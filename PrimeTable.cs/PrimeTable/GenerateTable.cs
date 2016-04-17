using System;
using System.Threading.Tasks;
using System.Threading;

namespace PrimeTable
{
    public class GenerateTable
    {
        private int requestNextRow;

        private int nextRowReady;

        private int nextColumnReady;

        private int requestNextColumn;

        private ulong columnLimit;

        private ulong columnNumber;

        private ulong rowNumber;

        private ulong rowLimit;

        private ulong tableSize;

        public ulong CurrentPrimeColumn { get; set; }

        public ulong Total { get; set; }

        public ulong CurrentPrimeRow { get; set; }

        public ulong Column { get; set; }

        public ulong Row { get; set; }

        public GenerateTable()
        {
            rowLimit = ulong.MaxValue;
            columnLimit = ulong.MaxValue;

            Task.Run(() => ComputePrimesRow());
            Task.Run(() => ComputePrimesColumn());
        }

        public void ResetColumnCount()
        {
            columnNumber = 2;
        }

        public void ResetRowCount()
        {
            rowNumber = 2;
        }

        /// <summary>
        ///     Find prime number for column by iterating from 2 to columnLimit.
        ///     Wait for next column request before calculating next prime number.
        ///     Once prime number is calculated call SetNextColumnReady() to 
        ///     indicate next prime number for column is ready.
        /// </summary>
        public void ComputePrimesColumn()
        {
            for (columnNumber = 2; columnNumber < columnLimit; columnNumber++)
            {
                int isPrime = IsPrimeNumber(columnNumber);

                if (isPrime != 2)
                {
                    WaitForNextColumnRequest();
                    CurrentPrimeColumn = columnNumber;
                    SetNextColumnReady();
                }
            }
        }
        
        public void SetNextColumnReady()
        {
            Interlocked.Increment(ref nextColumnReady);
        }

        /// <summary>
        ///     Find prime number for row by iterating from 2 to rowLimit.
        ///     Wait for next row request before calculating next prime number.
        ///     Once prime number is calculated call SetNextRowReady() to 
        ///     indicate next prime number for row is ready.
        /// </summary>
        public void ComputePrimesRow()
        {
            for (rowNumber = 2; rowNumber < rowLimit; rowNumber++)
            {
                int isPrime = IsPrimeNumber(rowNumber);

                if (isPrime != 2)
                {
                    WaitForNextRowRequest();
                    CurrentPrimeRow = rowNumber;
                    SetNextRowReady();
                }
            }
        }

        public void SetNextRowReady()
        {
            Interlocked.Increment(ref nextRowReady);
        }

        public void WaitForNextColumnRequest()
        {
            while (requestNextColumn <= 0)
            {
                Thread.Sleep(1);
            }
            requestNextColumn = 0;
        }

        /// <summary>
        ///     Determine if argument number is a prime number.
        /// </summary>
        /// <param name="number">A number to calculate if is prime.</param>
        /// <returns>Any number but 2 is a prime.</returns>
        public int IsPrimeNumber(ulong number)
        {
            int isPrime = 0;

            for (ulong y = 1; y < number; y++)
            {
                if (number % y == 0) { isPrime++; }

                if (isPrime == 2) { break; }
            }

            return isPrime;
        }

        public void WaitForNextRowRequest()
        {
            while (requestNextRow <= 0)
            {
                Thread.Sleep(1);
            }

            requestNextRow = 0;
        }

        public void RequestNextRow()
        {
            Interlocked.Increment(ref requestNextRow);
        }

        /// <summary>
        ///     Method to print prime table based on grid size value (n x n).
        /// </summary>
        /// <param name="gridSize">The size of the grid.</param>
        public void PrintTable(ulong gridSize)
        {
            if(gridSize < 2)
            {
                throw new ArgumentException("Grid size value must be greater than 2.");
            }

            tableSize = gridSize;

            PrintRowAndColumn();   
        }

        /// <summary>
        ///     Iterate through each row and get prime number for row and call PrintColumn().
        /// </summary>
        public void PrintRowAndColumn()
        {
            ResetRowCount();

            for (Row = 1; Row <= tableSize; Row++)
            {
                RequestNextRow();
                WaitForNextRowReady();

                // For displaying the first row, set currentPrimeRow = 1
                if (Row == 1)
                {
                    CurrentPrimeRow = 1;
                    PrintColumn();
                    ResetRowCount();
                    continue;
                }

                Console.WriteLine("\n");

                PrintColumn();
            }
        }

        public void WaitForNextRowReady()
        {
            while (nextRowReady <= 0)
            {
                Thread.Sleep(1);
            }
            nextRowReady = 0;
        }

        public void RequestNextColumn()
        {
            Interlocked.Increment(ref requestNextColumn);
        }

        /// <summary>
        ///     Iterate through each column and get prime number for column, calculate total and call WriteStdOut.
        /// </summary>
        public void PrintColumn()
        {
            ResetColumnCount();

            for (Column = 1; Column <= tableSize; Column++)
            {
                RequestNextColumn();
                WaitForNextColumnReady();

                // For displaying the first column, set currentPrimeColumn = 1
                if (Column == 1)
                {
                    CurrentPrimeColumn = 1;
                    Total = CalculateCurrentTotal();
                    WriteStdOut(Total);
                    ResetColumnCount();
                    continue;
                }

                Total = CalculateCurrentTotal();
                WriteStdOut(Total);
            }
        }

        public ulong CalculateCurrentTotal()
        {
            return CurrentPrimeRow * CurrentPrimeColumn;
        }

        /// <summary>
        ///     Write to console the value with different spacing based on table sizes.
        /// </summary>
        /// <param name="Total">Value to print.</param>
        public void WriteStdOut(ulong Total)
        {
            if (tableSize <= 66)
            {
                Console.Write("{0,6}", Total);
                return;
            }

            if (tableSize <= 700)
            {
                Console.Write("{0,8}", Total);
                return;
            }

            Console.Write("{0,20}", Total);
        }

        public void WaitForNextColumnReady()
        {
            while (nextColumnReady <= 0)
            {
                Thread.Sleep(1);
            }
            nextColumnReady = 0;
        }
    }
}
