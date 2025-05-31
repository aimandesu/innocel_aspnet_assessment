using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public static class NumberHelper
    {
        public static bool IsPrime(long number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            for (long i = 3; i * i <= number; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

    }
}