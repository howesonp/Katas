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
            if (inputNumber < 1 || inputNumber > 100) throw new ArgumentOutOfRangeException();

            if (inputNumber % 3 == 0 && inputNumber % 5 == 0) return "FIZZBUZZ";

            if (inputNumber % 3 == 0) return "FIZZ";

            if (inputNumber % 5 == 0) return "BUZZ";

            return inputNumber.ToString();
        }
    }
}
