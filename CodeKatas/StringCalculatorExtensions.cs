using System;
using System.Linq;

namespace CodeKatas
{
    public static class StringCalculatorExtensions
    {
        private const char Delimiter = ',';

        public static string ReplaceNewLinesWithDefaultDelimiter(this string stringNumbers)
        {
            var replacedStringNumbers =  stringNumbers.Replace(@"\n", Delimiter.ToString());

            return replacedStringNumbers;
        }

        public static void CheckForNegatives(this int[] numbers)
        {
            if (numbers.Any(parsedInputNumber => parsedInputNumber < 0))
            {
                var negativeNumbers = string.Join(Delimiter.ToString(), numbers.Where(parsedInputNumber => parsedInputNumber < 0));

                var exceptionMessage = $"negatives not allowed - { negativeNumbers }";

                throw new Exception(exceptionMessage);
            }
        }

        public static string[] SimplifyDelimitersWithDefault(this string stringNumbers)
        {
            var splitStringNumbers = stringNumbers.Split(Delimiter);

            if (splitStringNumbers[0].StartsWith(@"//"))
            {
                var variableDelimiter = splitStringNumbers[0].Remove(0, 2);
                splitStringNumbers[1] = splitStringNumbers[1].Replace(variableDelimiter, Delimiter.ToString());
                return splitStringNumbers[1].Split(Delimiter);
            }

            return splitStringNumbers;
        }

        public static int[] ConvertToNumbers(this string[] stringNumbers)
        {
            return stringNumbers.Select(int.Parse).ToArray();
        }
    }
}
