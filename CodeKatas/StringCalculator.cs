using System;
using System.Linq;

namespace CodeKatas
{
    public class StringCalculator
    {
        private const char Delimiter = ',';

        public int Add(string stringNumber)
        {
            if (string.IsNullOrEmpty(stringNumber))
            {
                return 0;
            }

            stringNumber.ReplaceNewLinesWithDefaultDelimiter();

            var stringNumbersArray = stringNumber.SimplifyDelimitersWithDefault();

            var numbers = stringNumbersArray.ConvertToNumbers();

            numbers.CheckForNegatives();

            return numbers.Sum();
        }
    }
}