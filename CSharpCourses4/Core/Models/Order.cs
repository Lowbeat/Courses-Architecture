namespace CSharpCourses4.Core.Models;

public class Order
{
    public Guid OrderId { get; set; }
    public Basket Basket { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}