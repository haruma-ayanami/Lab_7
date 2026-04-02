using System;
using System.IO;

namespace Lab_7
{
    // Базовый класс обработчика событий.
    // Реализует Template Method и является контекстом для Strategy.
    public abstract class EventHandlerBase
    {
        protected IFormatStrategy _strategy;

        protected EventHandlerBase(IFormatStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        // Позволяет менять стратегию форматирования во время выполнения.
        public void SetStrategy(IFormatStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        // Шаблонный метод: общая последовательность обработки события.
        public void ProcessEvent(MetricEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            string message = FormatMessage(e.EventType, e.Data);
            SendMessage(message);
            LogResult(e);
        }

        // Формирование сообщения с использованием текущей стратегии.
        protected virtual string FormatMessage(string type, object data)
        {
            string rawMessage = $"Событие: {type}. Данные: {data}";
            DateTime timestamp = data is MetricData metricData ? metricData.Timestamp : DateTime.Now;
            return _strategy.Format(rawMessage, timestamp);
        }

        // Отправка сообщения конкретным каналом.
        
        protected abstract void SendMessage(string message);

        // Дополнительное логирование результата.
        protected virtual void LogResult(MetricEventArgs e)
        {
            Console.WriteLine($"[LOG] {GetType().Name}: обработка события {e.EventType} завершена.");
        }
    }

    // Обработчик с выводом в консоль.
    public class ConsoleHandler : EventHandlerBase
    {
        public ConsoleHandler(IFormatStrategy strategy) : base(strategy)
        {
        }

        protected override void SendMessage(string message)
        {
            Console.WriteLine("[ConsoleHandler] Уведомление:");
            Console.WriteLine(message);
        }

        protected override void LogResult(MetricEventArgs e)
        {
            Console.WriteLine($"[ConsoleHandler] Сообщение по событию {e.EventType} успешно выведено в консоль.");
        }
    }

    // Обработчик с записью в файл.
    public class FileHandler : EventHandlerBase
    {
        private readonly string _filePath;

        public FileHandler(IFormatStrategy strategy, string filePath) : base(strategy)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        protected override void SendMessage(string message)
        {
            try
            {
                File.AppendAllText(_filePath, message + Environment.NewLine);
                Console.WriteLine($"[FileHandler] Сообщение записано в файл: {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileHandler] Ошибка записи в файл: {ex.Message}");
            }
        }

        protected override void LogResult(MetricEventArgs e)
        {
            Console.WriteLine($"[FileHandler] Обработка события {e.EventType} завершена.");
        }
    }

    // Обработчик с имитацией отправки email.
    public class EmailHandler : EventHandlerBase
    {
        private readonly string _email;

        public EmailHandler(IFormatStrategy strategy, string email) : base(strategy)
        {
            _email = email ?? throw new ArgumentNullException(nameof(email));
        }

        protected override void SendMessage(string message)
        {
            Console.WriteLine($"[EmailHandler -> {_email}] Имитация отправки email:");
            Console.WriteLine(message);
        }

        protected override void LogResult(MetricEventArgs e)
        {
            Console.WriteLine($"[EmailHandler] Email-уведомление по событию {e.EventType} отправлено.");
        }
    }
}