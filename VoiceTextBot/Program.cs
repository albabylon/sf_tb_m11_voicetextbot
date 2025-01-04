using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using VoiceTextBot;
using VoiceTextBot.Configuration;
using VoiceTextBot.Controllers;
using VoiceTextBot.Services;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;

        // Объект, отвечающий за постоянный жизненный цикл приложения
        var host = new HostBuilder()
            .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
            .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
            .Build(); // Собираем

        Console.WriteLine("Сервис запущен");

        // Запускаем сервис
        await host.RunAsync();
        Console.WriteLine("Сервис остановлен");
    }

    //Контейнер зависимостей
    private static void ConfigureServices(IServiceCollection services)
    {
        // Инициализация кофигурации
        AppSettings appSettings = BuildAppSettings();
        services.AddSingleton(appSettings);

        // Добавлено хранилеще сессий
        services.AddSingleton<IStorage, MemoryStorage>();

        // Подключаем контроллеры сообщений и кнопок
        services.AddTransient<DefaultMessageController>();
        services.AddTransient<VoiceMessageController>();
        services.AddTransient<TextMessageController>();
        services.AddTransient<InlineKeyboardController>();

        // Подключен обработчик файлов
        services.AddSingleton<IFileHandler, AudioFileHandler>();

        // Регистрируем объект TelegramBotClient c токеном подключения
        services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
        // Регистрируем постоянно активный сервис бота
        services.AddHostedService<Bot>();
    }

    private static AppSettings BuildAppSettings()
    {
        return new AppSettings()
        {
            DownloadsFolder = @"C:\Users\aveA\Downloads",
            BotToken = "7905728373:AAEgLElFIM5aFEhHW08nkzMPR6UJmpdsZUQ",
            AudioFileName = "audio",
            InputAudioFormat = "ogg",
            OutputAudioFormat = "wav",
            InputAudioBitrate = 48000,
        };
    }
}
