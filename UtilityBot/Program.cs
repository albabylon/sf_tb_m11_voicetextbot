using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using UtilityBot;

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

    static void ConfigureServices(IServiceCollection services)
    {
        // Регистрируем объект TelegramBotClient c токеном подключения
        services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("7898783423:AAFF0Y9QqOjeRWRRsHh8pZ7uIeXcpVmtVVI"));
        // Регистрируем постоянно активный сервис бота
        services.AddHostedService<Bot>();
    }
}
