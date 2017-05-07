using System;

namespace HashTable
{
    internal static class Util
    {
        public static int GetPrime(int min)
        {
            if (min <= 3)
            {
                return 3;
            }

            var result = min | 1;
            while (!IsPrime(result))
            {
                result+=2;
            }
            return result;
        }

        private static bool IsPrime(int candidate)
        {
            var limit = Math.Sqrt(candidate); 
            for (int i = 3; i <= limit; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
