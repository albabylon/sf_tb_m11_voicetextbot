namespace UtilityBot.Utilities
{
    public static class ConvertTextToNumbers
    {
        public static List<int> ToIntList(this string text)
        {
            string[] textNumbers = text.Split([',', ' ']);

            List<int> result = new List<int>();

            foreach (string tn in textNumbers)
            {
                if (int.TryParse(tn, out int number))
                    result.Add(number);
                else
                    continue;
            }

            return result;
        }
    }
}
