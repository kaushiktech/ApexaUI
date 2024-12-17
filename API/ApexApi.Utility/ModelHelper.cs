namespace ApexApi.Utility
{
    public static class ModelHelper
    {
        private static readonly Random getrandom = new Random();
        //1=Red,2=Yellow,3=Green
        public static int GenerateHealthStatus()
        {
            int rand = getrandom.Next(1, 3);
            return rand;
        }
    }
    public static class Masking
    {
        public static string MaskAllButLast(this string input, int charsToDisplay, char maskingChar = 'x')
        {
            if (!string.IsNullOrEmpty(input))
            {
                int charsToMask = input.Length - charsToDisplay;
                return charsToMask > 0 ? $"{new string(maskingChar, charsToMask)}{input.Substring(charsToMask)}" : input;
            }
            else
                return input;
        }
    }
}
