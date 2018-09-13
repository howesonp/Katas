using System;
using System.Linq;
using System.Runtime.CompilerServices;

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

            var rationalisedString = stringNumber.Replace(@"\n", Delimiter.ToString())
                                                 .Split(Delimiter);

            var numberStringsWithDefaultDelimiter = GetRationalisedStringNumberArray(rationalisedString);
        
            var arrayOfInts = numberStringsWithDefaultDelimiter.Select(int.Parse).ToArray();

            CheckForNegativeIntegersAndThrowExceptionIfFound(arrayOfInts);

            return arrayOfInts.Sum();
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
                var numbers = string.Join(",", arrayOfNumbers.Where(parsedInputNumber => parsedInputNumber < 0));

                var exceptionMessage = $"negatives not allowed - { numbers }";

                throw new Exception(exceptionMessage);
            }
        }
    }
}