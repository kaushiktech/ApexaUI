using System.Collections;
using System.Web.Http.ModelBinding;

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
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage).ToArray())
                                    .Where(m => m.Value.Any());
            }
            return null;
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
