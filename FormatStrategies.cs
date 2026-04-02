using System;

namespace Lab_7
{
    // Интерфейс стратегии форматирования.
    public interface IFormatStrategy
    {
        string Format(string message, DateTime timestamp);
    }

    // Текстовое форматирование.
    public class TextFormatStrategy : IFormatStrategy
    {
        public string Format(string message, DateTime timestamp)
        {
            return $"[{timestamp:yyyy-MM-dd HH:mm:ss}] {message}";
        }
    }

    // JSON-форматирование.
    public class JsonFormatStrategy : IFormatStrategy
    {
        public string Format(string message, DateTime timestamp)
        {
            string escapedMessage = message.Replace("\\", "\\\\").Replace("\"", "\\\"");
            return $"{{\"timestamp\":\"{timestamp:O}\",\"message\":\"{escapedMessage}\"}}";
        }
    }

    // HTML-форматирование.
    public class HtmlFormatStrategy : IFormatStrategy
    {
        public string Format(string message, DateTime timestamp)
        {
            string escapedMessage = message
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");

            return $"<div><strong>{timestamp:yyyy-MM-dd HH:mm:ss}</strong><br/><span>{escapedMessage}</span></div>";
        }
    }
}