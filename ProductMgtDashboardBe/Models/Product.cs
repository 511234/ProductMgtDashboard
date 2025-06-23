using System.ComponentModel.DataAnnotations;

namespace ProductMgtDashboardBe.Models;

public class Product
{
    [Key] public required string ProductCode { get; set; }

    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public int StockQuantity { get; set; }
    public required string Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
