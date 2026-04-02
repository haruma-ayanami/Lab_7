using System;

namespace Lab_7
{
    internal class Program
    {
        static void Main()
        {
            EventMonitor monitor = new EventMonitor();

            ConsoleHandler consoleHandler = new ConsoleHandler(new TextFormatStrategy());
            FileHandler fileHandler = new FileHandler(new JsonFormatStrategy(), "events.log");
            EmailHandler emailHandler = new EmailHandler(new HtmlFormatStrategy(), "admin@mail.com");

            // Подписка обработчиков на событие
            monitor.OnMetricExceeded += consoleHandler.ProcessEvent;
            monitor.OnMetricExceeded += fileHandler.ProcessEvent;
            monitor.OnMetricExceeded += emailHandler.ProcessEvent;

            Console.WriteLine("Проверка метрик");
            monitor.CheckMetric("CPU", 85, 80);
            Console.WriteLine();

            monitor.CheckMetric("Memory", 62, 75);
            Console.WriteLine();

            monitor.CheckMetric("Network", 1300, 1000);
            Console.WriteLine();

            monitor.CheckMetric("Disk", 91, 90);
            Console.WriteLine();

            Console.WriteLine("Демонстрация смены стратегии во время выполнения");
            consoleHandler.SetStrategy(new JsonFormatStrategy());

            monitor.CheckMetric("GPU", 97, 85);
            Console.WriteLine();

            Console.WriteLine("Демонстрация добавления новой метрики без изменения архитектуры");
            monitor.CheckMetric("Temperature", 78, 70);
            Console.WriteLine();

            Console.WriteLine("Работа программы завершена");
            Console.WriteLine("Проверьте файл events.log для просмотра уведомлений, записанных в файл.");
        }
    }
}