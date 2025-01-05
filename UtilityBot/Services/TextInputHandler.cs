using UtilityBot.Model;
using UtilityBot.Utilities;

namespace UtilityBot.Services
{
    public class TextInputHandler : IInputHandler
    {
        public string Process(string input, CountType userCountType)
        {
            string result = string.Empty;

            switch (userCountType)
            {
                case CountType.SumSymbols:
                    {
                        Console.WriteLine("Был запущен подсчет сиволов");
                        result = $"Результат вычисления количество символов: {input.Length.ToString()}";
                        break;
                    }
                case CountType.SumNumbers:
                    {
                        Console.WriteLine("Был запущен подсчет суммы чисел");
                        int sum = 0;
                        input.ToIntList().ForEach(n => sum += n);
                        result = $"Результат вычисления суммы чисел: {sum.ToString()}";
                        break;
                    }
            }

            return result;
        }
    }
}
