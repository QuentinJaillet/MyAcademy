namespace MyAcademy.Models;

public record Basket(IEnumerable<BasketItem> Items, decimal TotalPrice);