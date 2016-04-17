using System.Collections.Generic;
using System.Text;

namespace ReverseString
{
    public class StringManipulation
    {
        public string ReverseString(string input)
        {
            Stack<char> stackData = new Stack<char>();

            int length = input.Length;

            for (int i = 0; i < length; i++)
            {
                stackData.Push(input[i]);
            }

            string returnString = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(stackData.Pop());
            }

            return stringBuilder.ToString();
        }
    }
}
