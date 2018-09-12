using System.Linq;

namespace CodeKatas
{
    public class StringCalculator
    {
        public int Add(string stringNumber)
        {
            var stringNumberCommonDelimters = stringNumber.Replace("\n", ",");

            if (stringNumberCommonDelimters.Contains(","))
            {
                var splitNumbers = stringNumberCommonDelimters.Split(',');

                return splitNumbers.Sum(int.Parse);
            }

            var canParse = int.TryParse(stringNumber, out var parsedInt);

            if (canParse)
            {
                return parsedInt;
            }

            return 0;
        }
    }
}