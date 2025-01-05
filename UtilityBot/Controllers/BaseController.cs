using Telegram.Bot;

namespace UtilityBot.Controllers
{
    public abstract class BaseController
    {
        public readonly ITelegramBotClient _telegramClient;

        public BaseController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }
    }
}