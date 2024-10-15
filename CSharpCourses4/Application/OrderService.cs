using CSharpCourses4.Core.Models;

namespace CSharpCourses4.Application;

public class OrderService
{
    private readonly List<Order> _orders = new();

    public Guid CreateOrder(Basket basket)
    {
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            Basket = basket,
            Status = OrderStatus.Created,
            CreatedAt = DateTime.Now
        };
        _orders.Add(order);
        return order.OrderId;
    }

    public IEnumerable<Order> GetOrders() => _orders;

    public void UpdateOrderStatus()
    {
        foreach (var order in _orders.Where(o => o.Status < OrderStatus.Delivered))
        {
            order.Status++;
        }
    }

    public decimal CalculateBasketCost(Basket basket)
    {
        return basket.Dishes.Sum(d => d.Price);
    }
}
