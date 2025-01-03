﻿using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceTextBot.Configuration;

namespace VoiceTextBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramClient.SendMessage(message.Chat.Id, $"Получено текстовое сообщение", cancellationToken: ct);
        }
    }
}
