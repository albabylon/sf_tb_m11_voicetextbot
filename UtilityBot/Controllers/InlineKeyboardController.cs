using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Model;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class InlineKeyboardController : BaseController
    {
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage) : base(telegramBotClient)
        {
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            Console.WriteLine($"{callbackQuery.From.FirstName ?? "Аноним"}: {callbackQuery.Data}");

            if (Enum.TryParse(callbackQuery.Data, out CountType countTypeEnum))
                _memoryStorage.GetSession(callbackQuery.From.Id).CountType = countTypeEnum;
            else
                return;

            string countType = countTypeEnum switch
            {
                CountType.SumNumbers => "суммы чисел",
                CountType.SumSymbols => "суммы символов",
                _ => string.Empty
            };

            await _telegramClient.SendMessage(callbackQuery.From.Id,
                $"<b>Выбран подсчет {countType}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.",
                cancellationToken: ct,
                parseMode: ParseMode.Html);
        }
    }
}
