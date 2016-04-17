using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplePatternDetection;

namespace TestSimplePatternDetection
{
    [TestClass]
    public class UnitTest
    {
        Pattern pattern = new Pattern();
        
        [TestMethod]
        public void Smoke()
        {
            //bool result = pattern.PatternDetection("abca", "subs");
            bool result = pattern.PatternDetectionV1("abca", "abcaabcaabcaabcaabcaabca");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestNegativeDifferenceLength()
        {
            bool result = pattern.PatternDetectionV1("abca", "ss1sss1s");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestNegativeInvalidPattern()
        {
            bool result = pattern.PatternDetectionV1("abca", "aaaa");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestNegativeMinimalPattern()
        {
            try
            {
                bool result = pattern.PatternDetectionV1(string.Empty, string.Empty);
                Assert.IsTrue(result);
            }
            catch (ArgumentException e)
            {
                return;
            }
            
        }
    }
}
