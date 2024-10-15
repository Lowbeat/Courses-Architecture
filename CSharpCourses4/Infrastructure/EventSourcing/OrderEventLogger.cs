using CSharpCourses4.Core.Models;

namespace CSharpCourses4.Infrastructure.EventSourcing;

public class OrderEventLogger
{
    private const string LogFilePath = "OrderEvents.log";

    public void LogEvent(Order order)
    {
        var logEntry = $"{DateTime.Now}: Order {order.OrderId} status updated to {order.Status}";
        File.AppendAllLines(LogFilePath, new[] { logEntry });
    }
}
