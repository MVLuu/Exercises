using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestOfArrayProduct;

namespace TestRestOfArrayProduct
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Functional()
        {
            int[] values = new int[] { 1, 2, 3, 4 };
            Program program = new Program();
            int[] result = program.SkipIndexProductV1(values);
            int[] expectedResult = new int[] { 24, 12, 8, 6 };

            for(int i = 0; i < result.Length; i++)
            {
                if(!result[i].Equals(expectedResult[i]))
                {
                    Assert.Fail("Result failed to match expected result.");
                }
            }


            int[] result2 = program.SkipIndexProductV2(values);

            for (int i = 0; i < result2.Length; i++)
            {
                if (!result[i].Equals(expectedResult[i]))
                {
                    Assert.Fail("Result failed to match expected result.");
                }
            }

            int[] result3 = program.SkipIndexProductV3(values);

            for (int i = 0; i < result3.Length; i++)
            {
                if (!result[i].Equals(expectedResult[i]))
                {
                    Assert.Fail("Result failed to match expected result.");
                }
            }

            Console.Write("");
        }

        [TestMethod]
        public void LargeArray()
        {
            int[] values = CreateIntArray(10000);
            Program program = new Program();
            int[] result1 = program.SkipIndexProductV1(values);
            int[] result2 = program.SkipIndexProductV2(values);
            int[] result3 = program.SkipIndexProductV3(values);
            Console.Read();
        }

        public int[] CreateIntArray(int numberOfElements)
        {
            int[] result = new int[numberOfElements];
            Random r = new Random();
            for(int i = 0; i < numberOfElements; i++)
            {
                result[i] = r.Next(1, 100);
            }

            return result;
        }
    }
}
