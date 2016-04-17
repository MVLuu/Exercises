using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BirthdayCollision
{
    public class CollisionDetection
    {
        Dictionary<bool, int> result = new Dictionary<bool, int>();

        HashSet<int> birthdays = new HashSet<int>();

        Random random = new Random();

        public double PercentTile(int numberOfRuns, int numberOfPeople)
        {
            result.Clear();
            result.Add(false, 0);
            result.Add(true, 0);

            for (int i = 0; i < numberOfRuns; i++)
            {
                bool resultBool = Detection(numberOfPeople);
                result[resultBool] = result[resultBool] + 1;
            }

            return (double)result[true] / (double)numberOfRuns;
        }

        public bool Detection(int numberOfStudents)
        {
            birthdays.Clear();

            for (int i = 0; i < numberOfStudents; i++)
            {
                int birthday = CreateBirthday();

                if (birthdays.Contains(birthday))
                {
                    return true;
                }

                birthdays.Add(birthday);
            }

            return false;
        }

        public int CreateBirthday()
        {
            int dayOfBirth = 1;

            dayOfBirth = Next(1, 366);

            return dayOfBirth;
        }

        /// <summary>
        ///  Example of Next implementation from StackOverflow.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns>Random integer between minimum and maximum value.</returns>
        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            while (true)
            {
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    byte[] data = new byte[4];
                    rng.GetBytes(data);
                    uint rand = BitConverter.ToUInt32(data, 0);

                    Int64 max = (1 + (Int64)uint.MaxValue);
                    Int64 remainder = max % diff;
                    if (rand < max - remainder)
                    {
                        return (int)(minValue + (rand % diff));
                    }
                }
            }
        }
    }
}
