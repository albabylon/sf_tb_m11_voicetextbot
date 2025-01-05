using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using UtilityBot;
using UtilityBot.Configuration;
using UtilityBot.Controllers;
using UtilityBot.Services;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;

        var host = new HostBuilder()
            .ConfigureServices((hostContext, services) => ConfigureServices(services))
            .UseConsoleLifetime()
            .Build();

        Console.WriteLine("Сервис запущен");

        await host.RunAsync();
        Console.WriteLine("Сервис остановлен");
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        AppSetting appSettings = BuildAppSettings();
        services.AddSingleton(appSettings);

        services.AddSingleton<IStorage, MemoryStorage>();

        services.AddTransient<DefaultMessageController>();
        services.AddTransient<TextMessageController>();
        services.AddTransient<InlineKeyboardController>();

        services.AddSingleton<IInputHandler, TextInputHandler>();

        services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
        services.AddHostedService<Bot>();
    }

    private static AppSetting BuildAppSettings()
    {
        return new AppSetting()
        {
            BotToken = "7898783423:AAFF0Y9QqOjeRWRRsHh8pZ7uIeXcpVmtVVI",
        };
    }
}
