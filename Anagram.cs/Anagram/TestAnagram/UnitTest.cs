using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anagram;

namespace TestAnagram
{
    [TestClass]
    public class UnitTest
    {
        Anagrams anagrams = new Anagrams();
        Random random = new Random();

        [TestMethod]
        public void Smoke()
        {
            bool result = anagrams.IsAnagram("cat", "tac");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LongString()
        {
            string[] testData = AnagramGenerator(100);
            bool result = anagrams.IsAnagram(testData[0], testData[1]);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShortString()
        {
            bool result = anagrams.IsAnagram("a", "a");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidStringEmpty()
        {
            bool result = anagrams.IsAnagram(string.Empty, string.Empty);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InvalidMissmatchLength()
        {
            bool result = anagrams.IsAnagram("abc", "abcde");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InvalidCharacters()
        {
            bool result = anagrams.IsAnagram("~!@#$%^&*()", "~!@#$%^&*()");
            Assert.IsFalse(result);
        }

        private string[] AnagramGenerator(int numberOfCharacters)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] firstAnagramArray = new char[numberOfCharacters];
            for (int i = 0; i < firstAnagramArray.Length; i++)
            {
                firstAnagramArray[i] = characters[random.Next(characters.Length)];
            }
            
            char[] secondAnagramArray = new char[numberOfCharacters];
            firstAnagramArray.CopyTo(secondAnagramArray,0);
            FisherYatesShuffler(secondAnagramArray);

            string[] returnString = new string[2];
            returnString[0] = new string(firstAnagramArray);
            returnString[1] = new string(secondAnagramArray);

            return returnString;
        }

        private void FisherYatesShuffler<T>(T[] input)
        {
            int n = input.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + (int)(random.NextDouble() * (n - i));
                T t = input[r];
                input[r] = input[i];
                input[i] = t;
            }
        }
    }
}
