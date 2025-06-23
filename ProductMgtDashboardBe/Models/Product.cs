using System.ComponentModel.DataAnnotations;

namespace ProductMgtDashboardBe.Models;

public class Product
{
    [Key] public required string ProductCode { get; set; }

    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public int StockQuantity { get; set; }
    public ProductCategory Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

// public class ProductCategoryQuantityDTO
// {
//     public ProductCategoryQuantityDTO(ProductCategory category, int quantity)
//     {
//         Quantity = quantity;
//         CategoryId = (int)category;
//         CategoryName = "";
//     }

//     public int Quantity { get; set; }

//     public int CategoryId { get; set; }
//     public string CategoryName { get; set; }
// }