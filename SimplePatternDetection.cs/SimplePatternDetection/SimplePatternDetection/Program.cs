using System.Collections.Generic;
using System.Linq;
using System;

namespace SimplePatternDetection
{
    public class Pattern
    {
        static void Main(string[] args)
        {
            Pattern program = new Pattern();
            bool result = program.PatternDetectionV1("abca", "ss1s");
            Console.Write(result.ToString());
            Console.ReadLine();
        }

        public bool PatternDetectionV1(string pattern, string comparison)
        {
            int patternLength = pattern.Length;
            char[] patternArray = pattern.ToCharArray();
            
            int comparisonLength = comparison.Length;
            char[] comparisonArray = comparison.ToCharArray();
            
            if(patternLength == 0)
            {
                throw new ArgumentException("Pattern length needs to have at least one character.");
            }

            if (comparisonLength % patternLength != 0)
            {
                return false;
            }
            
            int numberOfIteration = comparisonLength / patternLength;

            List<string> splitComparison = new List<string>(numberOfIteration);
            int patternIndex = 0;
            for (int i = 0; i < numberOfIteration; i++)
            {
                splitComparison.Add(comparison.Substring(patternIndex, patternLength));
                patternIndex = patternIndex + patternLength;
            }

            Dictionary<char, string> patternDictionary = new Dictionary<char, string>();
            Dictionary<char, string> comparisonDictionary = new Dictionary<char, string>();

            var initialComparison = splitComparison[0];
            for (int i = 0; i < patternLength; i++)
            {
                if (!patternDictionary.ContainsKey(patternArray[i]))
                {
                    patternDictionary.Add(patternArray[i], i.ToString());
                }
                else
                {
                    patternDictionary[patternArray[i]] = patternDictionary[patternArray[i]] + "," + i.ToString();
                }
                var z = initialComparison.ToCharArray()[i];
                if (!comparisonDictionary.ContainsKey(z))
                {
                    comparisonDictionary.Add(initialComparison.ToCharArray()[i], i.ToString());
                }
                else
                {
                    comparisonDictionary[initialComparison.ToCharArray()[i]] = comparisonDictionary[initialComparison.ToCharArray()[i]] + "," + i.ToString();
                }
            }

            int iterations = patternDictionary.Count;
            // Remove one item in dictionary and compare the two values for matching pattern.
            for (int i = 0; i < iterations; i++)
            {
                if (!string.Equals(patternDictionary.First().Value, comparisonDictionary.First().Value))
                {
                    return false;
                }

                patternDictionary.Remove(patternDictionary.First().Key);
                comparisonDictionary.Remove(comparisonDictionary.First().Key);
            }

            foreach (string value in splitComparison)
            {
                if (!string.Equals(splitComparison[0], value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
