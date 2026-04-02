using System;

namespace Lab_7
{
    // Данные о метрике, по которой сработало событие.
    public class MetricData
    {
        public string MetricName { get; }
        public double Value { get; }
        public double Threshold { get; }
        public DateTime Timestamp { get; }

        public MetricData(string metricName, double value, double threshold, DateTime timestamp)
        {
            MetricName = metricName ?? throw new ArgumentNullException(nameof(metricName));
            Value = value;
            Threshold = threshold;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"Метрика: {MetricName}, Значение: {Value}, Порог: {Threshold}, Время: {Timestamp:yyyy-MM-dd HH:mm:ss}";
        }
    }
    
    // Аргументы события мониторинга.

    public class MetricEventArgs : EventArgs
    {
        public string EventType { get; }
        public MetricData Data { get; }

        public MetricEventArgs(string eventType, MetricData data)
        {
            EventType = eventType ?? throw new ArgumentNullException(nameof(eventType));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
    
    // Делегат обработчика события.
    public delegate void MetricEventHandler(MetricEventArgs e);
}