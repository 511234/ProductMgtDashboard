using System.ComponentModel.DataAnnotations;

namespace ProductMgtDashboardBe.Models.Entities;

public class Product
{
    // public Product(string productCode, string name, float price, int stockQuantity, ProductCategory category)
    // {
    //     ProductCode = productCode;
    //     Name = name;
    //     Price = price;
    //     StockQuantity = stockQuantity;
    //     Category = category;
    // }

    [Key] public required string ProductCode { get; set; }

    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public int StockQuantity { get; set; }
    public ProductCategory Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}