using UtilityBot.Model;

namespace UtilityBot.Services
{
    public interface IInputHandler
    {
        public string Process(string input, CountType countType);
    }
}
