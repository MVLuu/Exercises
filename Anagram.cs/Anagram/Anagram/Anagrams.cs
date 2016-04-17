using System;
using System.Collections.Generic;

namespace Anagram
{
    public class Anagrams
    {
        public bool IsAnagram(string firstInput, string secondInput)
        {
            foreach (var c in firstInput)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }

            foreach (var c in secondInput)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }

            if (firstInput.Length != secondInput.Length)
            {
                return false;
            }

            if(firstInput.Length == 0)
            {
                return false;
            }

            if (secondInput.Length == 0)
            {
                return false;
            }

            var firstInputArray = firstInput.ToLower().ToCharArray();
            var secondInputArray = secondInput.ToLower().ToCharArray();

            var firstInputDictionary = new Dictionary<char, int>();
            var secondInputDictionary = new Dictionary<char, int>();

            for (int i = 0; i < firstInputArray.Length; i++)
            {
                var firstInputChar = firstInputArray[i];

                int firstInputValue;
                if (firstInputDictionary.TryGetValue(firstInputChar, out firstInputValue))
                {

                    firstInputDictionary[firstInputChar] = firstInputValue + 1;
                }
                else
                {
                    firstInputDictionary.Add(firstInputChar, 1);
                }

                var secondInputChar = secondInputArray[i];
                int secondInputValue;
                if (secondInputDictionary.TryGetValue(secondInputChar, out secondInputValue))
                {
                    secondInputDictionary[secondInputChar] = secondInputValue + 1;
                }
                else
                {
                    secondInputDictionary.Add(secondInputChar, 1);
                }
            }

            foreach (KeyValuePair<char, int> entry in firstInputDictionary)
            {
                if (!secondInputDictionary.ContainsKey(entry.Key))
                {
                    return false;
                }

                if (entry.Value != secondInputDictionary[entry.Key])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
