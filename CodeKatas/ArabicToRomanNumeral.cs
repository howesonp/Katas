using System;
using System.Collections.Generic;

namespace CodeKatas
{
    public class ArabicToRomanNumeral
    {
        private readonly Dictionary<int, string> _arabicToRomanNumerals = new Dictionary<int, string>
        {
            {1000, "M"},
            {900, "CM"},
            {500, "D"},
            {400, "CD"},
            {100, "C"},
            {90, "XC"},
            {50, "L"},
            {40, "XL"},
            {10, "X"},
            {9, "IX"},
            {5, "V"},
            {4, "IV"},
            {1, "I"}
        };

        public string Convert(int arabicNumber)
        {
            if (arabicNumber < 1)
            {
                throw new Exception($"Roman numerals start at 1, can't convert {arabicNumber}");
            }

            var romanNumeralString = string.Empty;

            var arabicToRomanKeys = _arabicToRomanNumerals.Keys;

            foreach (var arabicToRomanKey in arabicToRomanKeys)
            {
                while (arabicNumber >= arabicToRomanKey)
                {
                    romanNumeralString += _arabicToRomanNumerals[arabicToRomanKey];

                    arabicNumber -= arabicToRomanKey;
                }
            }

            return romanNumeralString;
        }
    }
}