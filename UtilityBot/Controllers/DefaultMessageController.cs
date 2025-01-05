using Telegram.Bot;
using Telegram.Bot.Types;

namespace UtilityBot.Controllers
{
    public class DefaultMessageController : BaseController
    {
        public DefaultMessageController(ITelegramBotClient telegramBotClient) : base(telegramBotClient)
        {
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramClient.SendMessage(message.Chat.Id, $"Получено необрабатываемое сообщение. Введите /start для начала", cancellationToken: ct);
        }
    }
}
