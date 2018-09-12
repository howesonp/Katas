using System;
using System.Collections.Generic;

namespace CodeKatas
{
    public class FizzBuzzGame
    {
        public List<string> Play()
        {
            var returnVal = new List<string>();

            for (var i = 1; i < 100; i++)
            {
                returnVal.Add(CalculateFizzBuzz(i));
            }

            return returnVal;
        }

        public string CalculateFizzBuzz(int inputNumber)
        {
            const int minNumber = 1;
            const int maxNumber = 100;

            if (inputNumber < minNumber || inputNumber > maxNumber)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (inputNumber.IsDivisibleBy(3) && inputNumber.IsDivisibleBy(5))
            {
                return "FIZZBUZZ";
            }

            if (inputNumber.IsDivisibleBy(3))
            {
                return "FIZZ";
            }

            if (inputNumber.IsDivisibleBy(5))
            {
                return "BUZZ";
            }

            return inputNumber.ToString();
        }
    }
}
