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
}
