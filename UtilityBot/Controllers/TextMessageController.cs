using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Model;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class TextMessageController : BaseController
    {
        private readonly IStorage _memoryStorage;
        private readonly IInputHandler _inputHandler;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, IInputHandler inputHandler) : base(telegramBotClient)
        {
            _memoryStorage = memoryStorage;
            _inputHandler = inputHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"{message.From.FirstName ?? "Аноним"}: {message.Text}");

            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Посчитать символы", CountType.SumSymbols.ToString()),
                        InlineKeyboardButton.WithCallbackData($" Посчитать сумму", CountType.SumNumbers.ToString()),
                    });

                    await _telegramClient.SendMessage(message.Chat.Id,
                        $"<b>Бот может считать количество символов в введенном тексте или складывать введенные числа.</b> {Environment.NewLine}" + $"{Environment.NewLine}Выберите, что хотите сделать нажав соответствующую кнопку ниже.{Environment.NewLine}",
                        cancellationToken: ct,
                        parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    CountType userCountType = _memoryStorage.GetSession(message.Chat.Id).CountType;
                    string result = _inputHandler.Process(message.Text, userCountType);

                    await _telegramClient.SendMessage(message.Chat.Id, result, cancellationToken: ct);
                    break;
            }
        }
    }
}
