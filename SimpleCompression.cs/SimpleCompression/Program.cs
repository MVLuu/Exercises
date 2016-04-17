using System;

namespace Simplecompression
{
    class Program
    {
        static void Main(string[] args)
        {
            Program sc = new Program();
            string result = sc.SimpleCompression("kkeeee");
            Console.WriteLine(result);
            result = sc.SimpleCompression("k");
            Console.WriteLine(result);
            try
            {
                result = sc.SimpleCompression(string.Empty);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument Exception.");
            }
            result = sc.SimpleCompression("kt");
            Console.WriteLine(result);
            result = sc.SimpleCompression("kk");
            Console.WriteLine(result);
            result = sc.SimpleCompression("ktkeekkeekkee");
            Console.WriteLine(result);
            result = sc.SimpleCompression("eeeeee");
            Console.WriteLine(result);
            result = sc.SimpleCompression("eee eee");
            Console.WriteLine(result);
            result = sc.SimpleCompression("eteeeete");
            Console.WriteLine(result);
            result = sc.SimpleCompression("8888##'0()))) !#$%^&*&%$__---++===::::; ; ");
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public string SimpleCompression(string input)
        {
            char[] charArray = input.ToCharArray();
            int inputLength = charArray.Length;
            int repeated = 1;

            if (inputLength == 0)
            {
                throw new ArgumentException("Invalid input length, must more than one character.");
            }

            if (charArray.Length == 1)
            {
                return input + "1";
            }

            string returnString = charArray[0].ToString();

            for (int i = 1; i < charArray.Length; i++)
            {
                char currentChar = charArray[i];

                if (currentChar == charArray[i - 1])
                {
                    if(i == inputLength - 1)
                    {
                        repeated = repeated + 1;
                        return returnString + repeated;
                    }

                    repeated = repeated + 1;
                }

                if (currentChar != charArray[i - 1])
                {
                    if(inputLength -1 == i)
                    {
                        return returnString + "1" + currentChar + "1";
                    }
                    
                    returnString = returnString + repeated + currentChar;
                    repeated = 1;
                }
            }

            return returnString;
        }
    }
}
