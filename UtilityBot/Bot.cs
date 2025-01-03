using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace UtilityBot
{
    public class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramClient;

        public Bot(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // В аргументе 3 выбираем, какие обновления хотим получать. В данном случае разрешены все
            _telegramClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, new ReceiverOptions() { AllowedUpdates = { } }, cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен");
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //Нажатие на кнопки
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _telegramClient.SendMessage(update.CallbackQuery.From.Id, $"Сообщение не обработано. Введите, пожалуйста, текст.", cancellationToken: cancellationToken);
                return;
            }

            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        await _telegramClient.SendMessage(update.Message.From.Id, $"Длина сообщения: {update.Message.Text.Length} знаков", cancellationToken: cancellationToken);
                        return;
                    default:
                        await _telegramClient.SendMessage(update.Message.From.Id, $"Сообщение не обработано. Введите, пожалуйста, текст.", cancellationToken: cancellationToken);
                        return;
                }
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Задаем сообщение об ошибке в зависимости от того, какая именно ошибка произошла
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            // Выводим в консоль информацию об ошибке
            Console.WriteLine(errorMessage);

            // Задержка перед повторным подключением
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}
