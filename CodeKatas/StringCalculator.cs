using System;
using System.Linq;

namespace CodeKatas
{
    public class StringCalculator
    {
        public int Add(string stringNumber)
        {
            string rationalisedString;

            if (stringNumber.StartsWith(@"//"))
            {
                var delimiter = stringNumber.ToCharArray()[2];
                rationalisedString = stringNumber.Replace($@"//{delimiter}\n", string.Empty);
                rationalisedString = rationalisedString.Replace(delimiter.ToString(), ",");
            }
            else
            {
                rationalisedString = stringNumber.Replace("\n", ",");
            }
            
            if (rationalisedString.Contains(","))
            {
                var splitNumbers = rationalisedString.Split(',');

                return splitNumbers.Sum(int.Parse);
            }

            var canParse = int.TryParse(stringNumber, out var parsedInt);

            if (canParse)
            {
                if (parsedInt < 0)
                {
                    throw new Exception($"negatives not allowed - {parsedInt}");
                }

                return parsedInt;
            }

            return 0;
        }
    }
}