using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReverseString;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestReverseString
{
    [TestClass]
    public class UnitTest
    {
        StringManipulation stringManipulation;

        [TestInitialize]
        public void TestInitializeAttribute()
        {
            stringManipulation = new StringManipulation();
        }

        [TestMethod]
        public void Smoke()
        {
            string result = stringManipulation.ReverseString("Test");
            Assert.AreEqual("tseT", result);
        }

        [TestMethod]
        public void SpaceInString()
        {
            string result = stringManipulation.ReverseString("Te st aa a");
            Assert.AreEqual("a aa ts eT", result);
        }

        [TestMethod]
        public void SpaceMinimum()
        {
            string result = stringManipulation.ReverseString(string.Empty);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void MultipleIterations()
        {
            for (int i = 0; i < 100; i++)
            {
                string result = stringManipulation.ReverseString("Te st aa a");
                Assert.AreEqual("a aa ts eT", result);
            }
        }

        [TestMethod]
        public void NonStandardAscii1()
        {
            for (int i = 0; i < 100; i++)
            {
                string result = stringManipulation.ReverseString(@"~!@#$%^&*()_`{}|[]\:;'<>?,./");
                Assert.AreEqual(@"/.,?><';:\][|}{`_)(*&^%$#@!~", result);
            }
        }

        [TestMethod]
        public void NonStandardAscii2()
        {
            for (int i = 0; i < 100; i++)
            {
                string result = stringManipulation.ReverseString("è¼ÿ0D$hÀ");
                Assert.AreEqual("Àh$D0ÿ¼è", result);
            }
        }

        [TestMethod]
        public void ConcurrentRuns()
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                tasks.Add(Task.Run(() => { SampleReversalTest(); }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private void SampleReversalTest()
        {
            string sample = "The Run method allows you to create and execute a task in a single method call and is a simpler alternative to the StartNew method. It creates a task with the following default values";
            string expectedResult = "seulav tluafed gniwollof eht htiw ksat a setaerc tI .dohtem weNtratS eht ot evitanretla relpmis a si dna llac dohtem elgnis a ni ksat a etucexe dna etaerc ot uoy swolla dohtem nuR ehT";
            string result = stringManipulation.ReverseString(sample);
            Assert.AreEqual(expectedResult, result);   
        }
    }
}
