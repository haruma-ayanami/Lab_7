using System;

namespace Lab_7
{
    // Издатель событий мониторинга.
    // Реализация паттерна Observer через события C#.
    public class EventMonitor
    {
        // Событие превышения порогового значения метрики.
        public event MetricEventHandler? OnMetricExceeded;
        
        // Проверка метрики и генерация события при превышении порога.
        public void CheckMetric(string metricName, double value, double threshold)
        {
            if (string.IsNullOrWhiteSpace(metricName))
                throw new ArgumentException("Имя метрики не может быть пустым.", nameof(metricName));

            Console.WriteLine($"[Monitor] Проверка {metricName}: текущее значение = {value}, порог = {threshold}");

            if (value > threshold)
            {
                var eventData = new MetricData(
                    metricName: metricName,
                    value: value,
                    threshold: threshold,
                    timestamp: DateTime.Now
                );

                Console.WriteLine($"[Monitor] Обнаружено критическое событие: {metricName}");

                OnMetricExceeded?.Invoke(
                    new MetricEventArgs(
                        eventType: metricName + "_Exceeded",
                        data: eventData
                    )
                );
            }
            else
            {
                Console.WriteLine($"[Monitor] Метрика {metricName} в пределах нормы.");
            }
        }
    }
}