import time
import unittest
import PrimeTable
from PrimeTable import GenerateTable
import threading

class Test_GenerateTable(unittest.TestCase):

    def setUp(self):
        self.primeTable = GenerateTable()
        
    def test_sequential_run(self):
        """
        <summary>
            Run same set of test to generate a 10 x 10 prime grid
            20 times to confirm results are consistent.
        </summary>
        <steps>
            1. Create a for loop with 10 iterations.
            2. In the loop, generate a 10 x 10 prime grid.
        </steps>
        <verify>
            Verify the last prime column number is correct in each loop.
            Verify the last prime row number is correct in each loop.
        </verify>
        """
        numberOfRuns = 4
        for i in range(0, numberOfRuns):
            self.primeTable.printTable(10)
            expectedColumnValue = 23
            expectedRowValue = 23
            
            self.assertEqual(expectedColumnValue, self.primeTable.GetCurrentPrimeColumn(), "Last column header fails to match expected expected value.")
            self.assertEqual(expectedRowValue, self.primeTable.GetCurrentPrimeRow(), "Last row header fails to match expected expected value.")
            

    def test_default(self):
        """
        <summary>
             Run same set of test to generate a 10 x 10 prime grid
             20 times to confirm results are consistent.
        </summary>
        <steps>
             1. Create a for loop with 10 iterations.
             2. In the loop, generate a 10 x 10 prime grid.
        </steps>
        <verify>
             Verify the last prime column number is correct in each loop.
             Verify the last prime row number is correct in each loop.
        </verify>
        """
        primeTable = GenerateTable()
        primeTable.printTable(10)
        expectedColumnValue = 23
        expectedRowValue = 23
        self.assertEqual(expectedColumnValue, primeTable.GetCurrentPrimeColumn(), "Last column header fails to match expected expected value.")
        self.assertEqual(expectedRowValue, primeTable.GetCurrentPrimeRow(), "Last row header fails to match expected expected value.")
            
    def test_Row_And_Column_Calculation(self):
        """
        <summary>
            Confirm each cell in row and column are calculated correctly.
        </summary>
        <steps>
            1. Request a new row and get current prime number for row.
            2. Iterate through each column and get current prime number for column.
            3. Multiply current row prime number with current column prime number to get expected cell product.
            4. Call the program CalculationCurrentTotal method to get program total for cell.
        </steps>
        <verify>
             Confirm calculated product and product generate by CalculationCurrentTotal match.
        </verify>
        """
        primeTable = GenerateTable()
        primeTable.backgroundProcess()
        for j in range(0, 9):
            primeTable.requestNextRow()
            primeTable.waitForNextRowReady()

            for k in range(0, 9):
                
                
                primeTable.requestNextColumn()
                primeTable.waitForNextColumnReady()

                time.sleep(0.005)
                currentColumn = primeTable.GetCurrentPrimeColumn()
                currentRow = primeTable.GetCurrentPrimeRow()
                total = primeTable.calculateCurrentTotal()
                

                localTotal = currentColumn * currentRow
                self.assertEqual(total, localTotal)
                
    def test_Concurrent_Runs(self):
        """
        <summary>
            Test to confirm no impact on concurrency run.
        </summary>
        <steps>
           1. Generate and start multiple tasks (9).
           2. Wait for all tasks to complete.
        </steps>
        <verify>
           Confirm last current prime number for column is correct.
           Confirm last current prime number for row is correct.
        </verify>
        """
        self._threadDoneCounter = 0
        threadComputeOne = threading.Thread(target=self.generate_Table)
        threadComputeOne.daemon = True
        threadComputeOne.start()
        threadComputeTwo = threading.Thread(target=self.generate_Table)
        threadComputeTwo.daemon = True
        threadComputeTwo.start()
        threadComputeThree = threading.Thread(target=self.generate_Table)
        threadComputeThree.daemon = True
        threadComputeThree.start()

        while self._threadDoneCounter < 3:
            time.sleep(0.05)

    def generate_Table(self):
        primeTable = GenerateTable()
        primeTable.printTable(10)
        expectedColumnValue = 23
        expectedRowValue = 23
        self.assertEqual(expectedColumnValue, primeTable.GetCurrentPrimeColumn(), "Last column header fails to match expected expected value.")
        self.assertEqual(expectedRowValue, primeTable.GetCurrentPrimeRow(), "Last row header fails to match expected expected value.")
        self._threadDoneCounter += 1
        
    def test_High_Value(self):
        """
        <summary>
            Test to simulate a large prime grid of 100 x 100.
        </summary>
        <steps>
            1. Call PrintTable with 100 argument.
        </steps>
        <verify>
            Confirm last current prime column number is correct.
            Confirm last current prime row number is correct.
        </verify>
        """
        self.primeTable.printTable(100)
        expectedColumnValue = 523
        expectedRowValue = 523
        self.assertEqual(expectedColumnValue, self.primeTable.GetCurrentPrimeColumn(), "Last column header fails to match expected expected value.")
        self.assertEqual(expectedRowValue, self.primeTable.GetCurrentPrimeRow(), "Last row header fails to match expected expected value.")
        
    
    def test_Minimum_Value(self):
        """
        <summary>
            Test to confirm error handling of invalid argument.
        </summary>
        <steps>
            1. Pass a '0' value as argument for printTable.
        </steps>
        <verify>
            Epect a ValueError is raised.
        </verify>
        """
        try:
            self.primeTable = GenerateTable()
            self.primeTable.printTable(0)
        except ValueError:
            return
        self.fail("Expecting argument exception for low value.")
        
    def test_Below_Requirement_Value(self):
        """
        <summary>
            Test to confirm error handling of invalid argument.
        </summary>
        <steps>
            1. Pass a '1' value as argument for printTable.
        </steps>
        <verify>
            Expect a ValueError is raised.
        </verify>
        """
        try:
            self.primeTable = GenerateTable()
            self.primeTable.printTable(1)
        except ValueError:
            return
        self.fail("Expecting argument exception for low value.")

    def test_Invalid_Value(self):
        """
        <summary>
            Test to confirm error handling of invalid argument.
        </summary>
        <steps>
            1. Pass a "T" character as argument for printTable.
        </steps>
        <verify>
            Expect a ValueError is raised.
        </verify>
        """
        try:
            self.primeTable = GenerateTable()
            self.primeTable.printTable("T")
        except ValueError:
            return
        self.fail("Expecting argument exception for low value.")

    def test_negative_prime_number_calculation(self):
        """
        <summary>
            Negative test prime calculation method isPrimeNumber('value').
        </summary>
        <steps>
            1. Verify isPrimeNumber(53) is a valid prime number.
        </steps>
        <verify>
            Confirm result is True which indicate the value is a valid prime number.
        </verify>
        """
        primeNumber = 4;
        result = self.primeTable.isPrimeNumber(primeNumber)
        self.assertEqual(False, result);

    def test_prime_number_calculation(self):
        """
        <summary>
            Test prime calculation method isPrimeNumber('value').
        </summary>
        <steps>
            1. Verify isPrimeNumber(52) is not valid prime number.
        </steps>
        <verify>
            Confirm result is False which indicate the value is not a valid prime number.
        </verify>
        """
        primeNumber = 2;
        result = self.primeTable.isPrimeNumber(primeNumber)
        self.assertEqual(True, result);

    def test_prime_number_calculation(self):
        """
        <summary>
            Test prime calculation method isPrimeNumber('value').
        </summary>
        <steps>
            1. Verify isPrimeNumber(52) is not valid prime number.
        </steps>
        <verify>
            Confirm result is False which indicate the value is not a valid prime number.
        </verify>
        """
        primeNumber = 53;
        result = self.primeTable.isPrimeNumber(primeNumber)
        self.assertEqual(True, result);

    def test_Compute_Prime_Row(self):
        """
        <summary>
            Test to confirm the prime row calculation is correct.
        </summary>
        <steps>
            1. Iterate multiple times and get final current prime row value.
        </steps>
        <verify>
            Confirm the current prime row matches expected value.
        </verify>
        """
        primeTable = GenerateTable()
        primeTable.backgroundProcess()
        for i in range(0, 9):
            primeTable.requestNextRow()
            primeTable.waitForNextRowReady()
            time.sleep(0.001)
        self.assertEqual(23, primeTable.GetCurrentPrimeRow())

    def test_Compute_Prime_Column(self):
        """
        <summary>
            Test to confirm the prime column calculation is correct.
        </summary>
        <steps>
            1. Iterate multiple times and get the final current prime column value.
        </steps>
        <verify>
            Confirm the current prime column matches expected value.
        </verify>
        """
        primeTable = GenerateTable()
        primeTable.backgroundProcess()
        for i in range(0, 9):
            primeTable.requestNextColumn()
            primeTable.waitForNextColumnReady()
            time.sleep(0.001)
        self.assertEqual(23, primeTable.GetCurrentPrimeColumn())
        
if __name__ == "__main__":
    unittest.main()
    numberOfRuns = 20
