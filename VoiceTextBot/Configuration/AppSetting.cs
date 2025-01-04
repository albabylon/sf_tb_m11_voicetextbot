﻿namespace VoiceTextBot.Configuration
{
    public class AppSettings
    {
        /// <summary>
        /// Токен Telegram API
        /// </summary>
        public string BotToken { get; set; }

        /// <summary>
        /// Папка загрузки аудио файлов
        /// </summary>
        public string DownloadsFolder { get; set; }
        /// <summary>
        /// Имя файла при загрузке
        /// </summary>
        public string AudioFileName { get; set; }
        /// <summary>
        /// Формат аудио при загрузке
        /// </summary>
        public string InputAudioFormat { get; set; }
        
        /// <summary>
        /// Формат аудио при выгрузке
        /// </summary>
        public string OutputAudioFormat { get; set; }
        
        /// <summary>
        /// Битрейтр
        /// </summary>
        public float InputAudioBitrate { get; set; }
}
}
