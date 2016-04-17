import asyncio
import datetime
import time
import threading
import sys

class GenerateTable(object):

    def isPrimeNumber(self, number):

        isPrime = 0;
        for y in range(1, number):
            if number % y == 0:
                isPrime += 1
            if isPrime == 2:
                return False
        if isPrime == 2:
            return False
        return True
        
        
    def computePrimesForRow(self):
        self.currentPrimeRow = 0
        while self._running == True:
            if self.rowNumber == 0:
                self.rowNumber = 1
                self.rowValue = 2

            if self.isPrimeNumber(self.rowValue):
                while self._requestNextRow == False:
                    time.sleep(0)
                self._requestNextRow = False
                self.currentPrimeRow = self.rowValue
                self._nextRowReady = True
                
            self.rowValue += 1
                
    def computePrimesForColumn(self):
        self.currentPrimeColumn = 0
        while self._running == True:
            if self.columnNumber == 0:
                self.columnNumber = 1
                self.columnValue = 2
                
            if self.isPrimeNumber(self.columnValue):
                while self._requestNextColumn == False:
                    time.sleep(0)
                self._requestNextColumn = False
                self.currentPrimeColumn = self.columnValue
                self._nextColumnReady = True
                
            self.columnValue += 1
        
    def requestNextRow(self):
        self._requestNextRow = True

    def requestNextColumn(self):
        self._requestNextColumn = True

    def waitForNextRowReady(self):
        while self._nextRowReady == False:

                if self._running == True:
                    continue
                else:
                    break

    def waitForNextColumnReady(self):
        while self._nextColumnReady == False:

                if self._running == True:
                    continue
                else:
                    break
        
    def printRowAndColumn(self):
        self.rowNumber = 0
        for row in range(1, self.tableSize):

            self.requestNextRow()
            self.waitForNextRowReady()
            
            self._nextRowReady = False
            self._requestNextRow = False
            
            if row == 1:
                self.currentPrimeRow = 1
                self.printColumn()
                continue
            
            print("\n")
            self.printColumn()
        
    def printColumn(self):
        self.columnNumber = 0
        for column in range(1, self.tableSize):

            self.requestNextColumn()
            self.waitForNextColumnReady()
                
            self._nextColumnReady = False
            self._requestNextColumn = False
            
            if column == 1:
                self.currentPrimeColumn = 1
                self.writeStdOut(self.calculateCurrentTotal())
                continue
            
            self.writeStdOut(self.calculateCurrentTotal())

    def GetCurrentPrimeColumn(self):
        return self.currentPrimeColumn

    def GetCurrentPrimeRow(self):
        return self.currentPrimeRow
        
    def calculateCurrentTotal(self):
        return self.GetCurrentPrimeColumn() * self.GetCurrentPrimeRow()

    def writeStdOut(self, Total):
        sys.stdout.write("{0:6d}".format(Total))
    
    def printTable(self, gridSize):
        print("\n")
        if self._running == False:
            self.backgroundProcess()
        
        self.rowNumber = 0
        if int(gridSize) < 2:
            raise ValueError('The value is not valid. \nPlease enter a number 2 or greater.')
        self.tableSize = int(gridSize) + 1
        self.printRowAndColumn()
        self.terminate()
        
    def terminate(self):
        self._running = False
        self.columnNumber = 0

    def backgroundProcess(self):
        self._running = True
        threadComputePrimesColumn = threading.Thread(target=self.computePrimesForRow)
        threadComputePrimesColumn.daemon = True
        threadComputePrimesColumn.start()
        threadComputePrimesRow = threading.Thread(target=self.computePrimesForColumn)
        threadComputePrimesRow.daemon = True
        threadComputePrimesRow.start()
        

    def __init__(self):
        self._running = False
        self._requestNextRow = False
        self._requestNextColumn = False
        self._nextColumnReady = False
        self._nextRowReady = False
        self.currentPrimeColumn = 0
        self.currentPrimeRow = 0
        self.columnNumber = 0
        self.rowNumber = 0
        self.tableSize = 0
        self.columnValue = 2
        self.rowValue = 2
                
if __name__ == "__main__":
    primeTable = GenerateTable()
    time.sleep(0.5)
    try:
        inputData = input("Type table size or Enter for default table size of 10: ")
        inputNumber = int(inputData)
        primeTable.printTable(inputData)
        print("T")
    except ValueError:
        print("DD")
    while inputNumber <= 1:
        print("C")
        try:
            print("E")
            inputData = input("The value is not valid. \nPlease enter a number 2 or greater.")
            inputNumber = int(inputData)
            primeTable.printTable(inputNumber)
        except ValueError:
            print("The value is not valid. \nPlease enter a number 2 or greater.")
            
    
    
