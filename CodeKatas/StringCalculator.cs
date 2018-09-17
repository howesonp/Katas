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

            var stringNumberNewLinesReplaced = stringNumber.Replace(@"\n", Delimiter.ToString())
                                                 .Split(Delimiter);

            var numberStrings = GetRationalisedStringNumberArray(stringNumberNewLinesReplaced);
        
            var arrayOfInputInts = numberStrings.Select(int.Parse).ToArray();

            CheckForNegativeIntegersAndThrowExceptionIfFound(arrayOfInputInts);

            return arrayOfInputInts.Sum();
        }

        private static string[] GetRationalisedStringNumberArray(string[] arrayOfStringNumbers)
        {
            if (arrayOfStringNumbers[0].StartsWith(@"//"))
            {
                var variableDelimiter = arrayOfStringNumbers[0].Remove(0, 2);
                arrayOfStringNumbers[1] = arrayOfStringNumbers[1].Replace(variableDelimiter, Delimiter.ToString());
                return arrayOfStringNumbers[1].Split(Delimiter);
            }

            return arrayOfStringNumbers;
        }

        private static void CheckForNegativeIntegersAndThrowExceptionIfFound(int[] arrayOfNumbers)
        {
            if (arrayOfNumbers.Any(parsedInputNumber => parsedInputNumber < 0))
            {
                var numbers = string.Join(Delimiter.ToString(), arrayOfNumbers.Where(parsedInputNumber => parsedInputNumber < 0));

                var exceptionMessage = $"negatives not allowed - { numbers }";

                throw new Exception(exceptionMessage);
            }
        }
    }
}