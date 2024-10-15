using CSharpCourses4.Application;
using CSharpCourses4.Core.Models;
using CSharpCourses4.Infrastructure.DI;
using CSharpCourses4.Infrastructure.EventSourcing;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpCourses4.Presentation;

public class Program
{
    private static OrderService _orderService;
    private static OrderEventLogger _eventLogger;

    public static async Task Main(string[] args)
    {
        var provider = DependencyInjection.Configure();
        _orderService = provider.GetRequiredService<OrderService>();
        _eventLogger = provider.GetRequiredService<OrderEventLogger>();

        await StartOrderStatusUpdateSimulation();

        while (true)
        {
            Console.WriteLine("Options:");
            Console.WriteLine("1. Create Order");
            Console.WriteLine("2. Show Orders");
            Console.WriteLine("3. Exit");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateOrder();
                    break;
                case "2":
                    ShowOrders();
                    break;
                case "3":
                    return;
            }
        }
    }

    private static void CreateOrder()
    {
        var basket = new Basket();
        switch (new Random().Next(1, 5))
        {
            case 1:
                basket.Dishes.Add(Dishes.Pizza);
                break;
            case 2:
                basket.Dishes.Add(Dishes.Sushi);
                break;
            case 3:
                basket.Dishes.Add(Dishes.ChickenSoup);
                break;
            case 4:
                basket.Dishes.Add(Dishes.Pizza);
                basket.Dishes.Add(Dishes.Sushi);
                basket.Dishes.Add(Dishes.ChickenSoup);
                break;
        }
        var orderId = _orderService.CreateOrder(basket);

        Console.WriteLine($"Order {orderId} created with dishes: {string.Join(", ", basket.Dishes.Select(x => x.Name))}. Total amount: {_orderService.CalculateBasketCost(basket)} rub.");
    }

    private static void ShowOrders()
    {
        var orders = _orderService.GetOrders().ToList();
        if (orders.Any())
        {
            foreach (var order in orders)
            {
                Console.WriteLine($"Order {order.OrderId}, Status: {order.Status}, Dishes: {string.Join(", ", order.Basket.Dishes.Select(x => x.Name))}");
                _eventLogger.LogEvent(order);
            }
        }
        else
        {
            Console.WriteLine("Order list is empty for now! Create a new order.");
        }
    }

    private static async Task StartOrderStatusUpdateSimulation()
    {
        await Task.Run(() =>
        {
            var timer = new System.Timers.Timer(5000);
            timer.Elapsed += (sender, e) => _orderService.UpdateOrderStatus();
            timer.Start();
        });
    }
}