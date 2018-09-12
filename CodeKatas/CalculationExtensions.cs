namespace CodeKatas
{
    public static class CalculationExtensions
    {
        public static bool IsDivisibleBy(this int inputNumber, int divisibleBy)
        {
            return inputNumber % divisibleBy == 0;
        }
    }
}