using System;

namespace Anagram
{
    class Program
    {
        static void Main(string[] args)
        {
            Anagrams anagrams = new Anagrams();
            bool result = anagrams.IsAnagram("cat", "tac");
            Console.WriteLine(result);
        }
    }
}

