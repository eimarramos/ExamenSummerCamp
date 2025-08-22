namespace Domain.Entities;

public class Gadget
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; 
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}
