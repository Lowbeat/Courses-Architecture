using CSharpCourses4.Application;
using CSharpCourses4.Infrastructure.EventSourcing;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpCourses4.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();
        services.AddSingleton<OrderService>();
        services.AddSingleton<OrderEventLogger>();
        return services.BuildServiceProvider();
    }
}