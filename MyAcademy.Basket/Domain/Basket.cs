namespace MyAcademy.Basket.Domain;

public class Basket
{
    public Guid Id { get; set; }
    public List<BasketItem> Items { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public void AddItem(Guid courseId, decimal price)
    {
        // Vérifie si un item avec le même CourseId existe déjà
        var existingItem = Items.FirstOrDefault(x => x.CourseId == courseId);
        if (existingItem == null)
        {
        }
        else
        {
            Items.Add(new BasketItem
            {
                Id = Guid.NewGuid(),
                CourseId = courseId,
                Price = price
            });
        }
    }
}

public class BasketItem
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public decimal Price { get; set; }
}