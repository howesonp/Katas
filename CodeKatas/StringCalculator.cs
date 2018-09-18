using System.Linq;

namespace CodeKatas
{
    public class StringCalculator
    {
        public int Add(string stringNumber)
        {
            if (string.IsNullOrEmpty(stringNumber))
            {
                return 0;
            }

            var stringNumbersWithoutNewLines = stringNumber.ReplaceNewLinesWithDefaultDelimiter();

            var stringNumbers = stringNumbersWithoutNewLines.SimplifyDelimitersWithDefault();

            var numbers = stringNumbers.ConvertToNumbers();

            numbers.CheckForNegatives();

            return numbers.Sum();
        }
    }
}