using CSharpCourses4.Core.Models;

namespace CSharpCourses4.Core.Commands;

public class CreateOrderCommand
{
    public Basket Basket { get; set; }
}