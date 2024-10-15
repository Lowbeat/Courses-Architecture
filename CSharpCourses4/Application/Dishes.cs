using CSharpCourses4.Core.Models;

namespace CSharpCourses4.Application;

public static class Dishes
{
    public static Dish Pizza = new Dish { Id = Guid.NewGuid(), Name = "Pizza", Price = 500 };
    public static Dish Sushi = new Dish { Id = Guid.NewGuid(), Name = "Sushi", Price = 300 };
    public static Dish ChickenSoup = new Dish { Id = Guid.NewGuid(), Name = "Chicken soup", Price = 150 };
}