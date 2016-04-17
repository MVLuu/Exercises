using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeTable;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PrimeTableTest
{
    [TestClass]
    public class UnitTests
    {
        GenerateTable generateTable;
        
        public UnitTests()
        {
            generateTable = new GenerateTable();
        }

        /// <summary>
        ///     Sunny day scenario to cover basic requirement of program.
        /// </summary>
        /// <steps>
        ///     1. Print prime table with 10 x 10 grid.
        /// </steps>
        /// <verify>
        ///     Verify the last current prime number for column is correct.
        ///     Verify the last current prime number for row is correct.
        /// </verify>
        [TestMethod]
        public void TestDefaultValue()
        {
            generateTable.PrintTable(10);

            ulong column = 23;
            ulong row = 23;
            Assert.AreEqual(column, generateTable.CurrentPrimeColumn);
            Assert.AreEqual(row, generateTable.CurrentPrimeRow);
        }

        /// <summary>
        ///     Run same set of test to generate a 10 x 10 prime grid
        ///     20 times to confirm results are consistent.
        /// </summary>
        /// <steps>
        ///     1. Create a for loop with 10 iterations.
        ///     2. In the loop, generate a 10 x 10 prime grid.
        /// </steps>
        /// <verify>
        ///     Verify the last prime column number is correct in each loop.
        ///     Verify the last prime row number is correct in each loop.
        /// </verify>
        [TestMethod]
        public void TestSequentialRun()
        {
            int numberOfRuns = 20;
            for (int i = 0; i < numberOfRuns; i++)
            {
                generateTable.PrintTable(10);

                ulong column = 23;
                ulong row = 23;
                Assert.AreEqual(column, generateTable.CurrentPrimeColumn);
                Assert.AreEqual(row, generateTable.CurrentPrimeRow);
            }
        }

        /// <summary>
        /// Confirm each cell in row and column are calculated correctly.
        /// </summary>
        /// <steps>
        ///     1. Request a new row and get current prime number for row.
        ///     2. Iterate through each column and get current prime number for column.
        ///     3. Multiply current row prime number with current column prime number to get expected cell product.
        ///     4. Call the program CalculationCurrentTotal method to get program total for cell.
        /// </steps>
        /// <verify>
        ///     Confirm calculated product and product generate by CalculationCurrentTotal match.
        /// </verify>
        [TestMethod]
        public void TestRowAndColumnCalculation()
        {
            for (int j = 0; j < 9; j++)
            {
                generateTable.RequestNextRow();
                generateTable.WaitForNextRowReady();

                for (int i = 0; i < 9; i++)
                {

                    generateTable.RequestNextColumn();
                    generateTable.WaitForNextColumnReady();

                    var total = generateTable.CalculateCurrentTotal();

                    var currentColumn = generateTable.CurrentPrimeColumn;
                    var currentRow = generateTable.CurrentPrimeRow;

                    var localTotal = currentColumn * currentRow;

                    Assert.AreEqual(total, localTotal);
                }
                
                generateTable.ResetColumnCount();
            }
        }
        
        /// <summary>
        ///     Test to confirm no impact on concurrency run.
        /// </summary>
        /// <steps>
        ///     1. Generate and start multiple tasks (9).
        ///     2. Wait for all tasks to complete.
        /// </steps>
        /// <verify>
        ///     Confirm last current prime number for column is correct.
        ///     Confirm last current prime number for row is correct.
        /// </verify>
        [TestMethod]
        public void TestConcurrenteRuns()
        {

            List<Task> TaskList = new List<Task>();

            int numberOfCurrentRuns = 9;
            for (int q = 0; q < numberOfCurrentRuns; q++)
            {
                Task T = new Task(GenerateTable);
                T.Start();
                
                TaskList.Add(T);
            }

            Task.WaitAll(TaskList.ToArray());
        }

        private void GenerateTable()
        {
            var generateTable = new GenerateTable();
            generateTable.PrintTable(10);
            ulong column = 23;
            ulong row = 23;
            Assert.AreEqual(column, generateTable.CurrentPrimeColumn);
            Assert.AreEqual(row, generateTable.CurrentPrimeRow);
        }

        /// <summary>
        ///     Test to simulate a large prime grid of 100 x 100.
        /// </summary>
        /// <steps>
        ///     1. Call PrintTable with 100 argument.
        /// </steps>
        /// <verify>
        ///     Confirm last current prime column number is correct.
        ///     Confirm last current prime row number is correct.
        /// </verify>
        [TestMethod]
        public void TestHighValue()
        {
            generateTable.PrintTable(100);

            ulong column = 523;
            ulong row = 523;
            Assert.AreEqual(column, generateTable.CurrentPrimeColumn);
            Assert.AreEqual(row, generateTable.CurrentPrimeRow);
        }

        /// <summary>
        ///     Test to confirm error handling of invalid argument.
        /// </summary>
        /// <steps>
        ///     1. Call PrintTable with ulong.MinValue.
        /// </steps>
        /// <verify>
        ///     Verify that an invalid argument exception is generated.
        /// </verify>
        [TestMethod]
        public void TestMinimumValue()
        {
            try
            {
                generateTable.PrintTable(ulong.MinValue);
            }
            catch (ArgumentException e)
            {
                return;
            }

            Assert.Fail("Expecting argument exception for low value.");
        }

        /// <summary>
        ///     Test to validate below minimum requirement for PrintTable argument.
        /// </summary>
        /// <steps>
        ///     1. Insert a 1 value as argument for PrintTable.
        /// </steps>
        /// <verify>
        ///     Verify an invalid argument exception is generated.
        /// </verify>
        [TestMethod]
        public void TestBelowRequirement()
        {
            try
            {
                ulong value = 1;
                generateTable.PrintTable(value);
            }
            catch (ArgumentException e)
            {
                return;
            }

            Assert.Fail("Expecting argument exception for low value.");
        }

        /// <summary>
        ///     Test prime calculation method IsPrimeNumber('value').
        /// </summary>
        /// <steps>
        ///     1. Call IsPrimeNumber with a valid prime number.
        /// </steps>
        /// <verify>
        ///     Confirm result is a 1 value which indicate the value is a valid prime number.
        /// </verify>
        [TestMethod]
        public void TestPrimeCalculation()
        {
            ulong primeNumber = 53;
            var result = generateTable.IsPrimeNumber(primeNumber);
            int validPrimeCode = 1;
            Assert.AreEqual(validPrimeCode, result);
        }

        /// <summary>
        ///     Test prime calculation method IsPrimeNumber('value'), negative test.
        /// </summary>
        /// <steps>
        ///     1. Call IsPrimeNumber with an invalid prime number.
        /// </steps>
        /// <verify>
        ///     Confirm result is a 2 value which indicate the value is not a prime number.
        /// </verify>
        [TestMethod]
        public void TestNegativePrimeCalculation()
        {
            ulong primeNumber = 52;
            var result = generateTable.IsPrimeNumber(primeNumber);
            Assert.AreEqual(2, result);
        }

        /// <summary>
        ///     Test to confirm the prime row calculation is correct.
        /// </summary>
        /// <steps>
        ///     1. Iterate multiple times and get final current prime row value.
        /// </steps>
        /// <verify>
        ///     Confirm the current prime row matches expected value.
        /// </verify>
        [TestMethod]
        public void TestComputePrimeRow()
        {
            for (int i = 1; i <= 9; i++)
            {
                generateTable.RequestNextRow();
                generateTable.WaitForNextRowReady();
            }

            ulong expectedResult = 23;
            Assert.AreEqual(expectedResult, generateTable.CurrentPrimeRow);
        }

        /// <summary>
        ///     Test to confirm the prime column calculation is correct.
        /// </summary>
        /// <steps>
        ///     1. Iterate multiple times and get the final current prime column value.
        /// </steps>
        /// <verify>
        ///     Confirm the current prime column matches expected value.
        /// </verify>
        [TestMethod]
        public void TestComputePrimeColumn()
        {
            for(int i = 1; i <= 9; i++)
            {
                generateTable.RequestNextColumn();
                generateTable.WaitForNextColumnReady();
            }

            ulong expectedResult = 23;
            Assert.AreEqual(expectedResult, generateTable.CurrentPrimeColumn);
        }
    }
}
