﻿using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceTextBot.Configuration;
using VoiceTextBot.Models;
using VoiceTextBot.Services;

namespace VoiceTextBot.Controllers
{
    public class VoiceMessageController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(AppSettings appSettings, ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler, IStorage memoryStorage)
        {
            _memoryStorage = memoryStorage;
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;

            await _audioFileHandler.Download(fileId, ct);

            string userLanguageCode = _memoryStorage.GetSession(message.Chat.Id).LanguageCode; // Здесь получим язык из сессии пользователя
            var result = _audioFileHandler.Process(userLanguageCode); // Запустим обработку
            await _telegramClient.SendMessage(message.Chat.Id, result, cancellationToken: ct);
        }
    }
}
