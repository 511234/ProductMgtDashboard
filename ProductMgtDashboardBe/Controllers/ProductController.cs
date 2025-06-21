using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMgtDashboardBe.Data;
using ProductMgtDashboardBe.Models;
using ProductMgtDashboardBe.Models.Entities;

namespace ProductMgtDashboardBe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    //Read all
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        try
        {
            var products = await _applicationDbContext.Products.ToListAsync();
            return Ok(products);
        }

        catch (Exception e)
        {
            return BadRequest("Error when getting products");
        }
    }

    // Get product by productCode
    [HttpGet("{productCode}")]
    public async Task<ActionResult<Product>> GetProduct(string productCode)
    {
        try
        {
            var product = await _applicationDbContext.Products.FirstOrDefaultAsync(e => e.ProductCode == productCode);
            if (product == null) return NotFound($"Product with product code {productCode} not found");
            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest("Error when getting product");
        }
    }

    // Get total product quantity of all categories, grouped by category
    [HttpGet("quantity/all")]
    public async Task<ActionResult<Product>> GetProductQuantityAllCategories()
    {
        var productCountList = await _applicationDbContext.Products
            .GroupBy(p => p.Category)
            .Select(group => new
            {
                categoryId = group.Key,
                quantity = group.Count()
            })
            .ToListAsync();

        var result = productCountList
            .Select(g => new
            {
                CategoryId = (int)g.categoryId,
                CategoryName = Enum.GetName(typeof(ProductCategory), g.categoryId),
                Quantity = g.quantity
            })
            .ToList();
        return Ok(result);
    }

    // Add a new product
    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product newProduct)
    {
        try
        {
            var existingProduct =
                await _applicationDbContext.Products.FirstOrDefaultAsync(e => e.ProductCode == newProduct.ProductCode);
            if (existingProduct != null)
                return UnprocessableEntity(
                    $"Fail to create product. Product with product code {newProduct.ProductCode} already exists");
            _applicationDbContext.Products.Add(newProduct);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(newProduct);
        }
        catch (Exception e)
        {
            return BadRequest("Error when creating product");
        }
    }

    // Delete a product
    [HttpDelete("{productCode}")]
    public async Task<ActionResult<Product>> DeleteProduct(string productCode)
    {
        try
        {
            var product = await _applicationDbContext.Products.FirstOrDefaultAsync(e => e.ProductCode == productCode);
            if (product == null) return NotFound($"Product with product code {productCode} not found");

            _applicationDbContext.Products.Remove(product);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest("Error when deleting product");
        }
    }


    [HttpPut]
    public async Task<ActionResult<Product>> UpdateProduct(Product updatedProduct)
    {
        try
        {
            var product =
                await _applicationDbContext.Products.FirstOrDefaultAsync(e =>
                    e.ProductCode == updatedProduct.ProductCode);

            if (product == null) return NotFound($"Product with product code {updatedProduct.ProductCode} not found");

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.StockQuantity = updatedProduct.StockQuantity;
            product.Category = updatedProduct.Category;

            await _applicationDbContext.SaveChangesAsync();

            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest("Error when updating product");
        }
    }
}