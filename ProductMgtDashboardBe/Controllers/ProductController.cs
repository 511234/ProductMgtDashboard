using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMgtDashboardBe.Database;
using ProductMgtDashboardBe.Models.Entities;

namespace ProductMgtDashboardBe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductController(ApplicationDbContext applicationDbContext)
    {
        this._applicationDbContext = applicationDbContext;
    }

    //Read all
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _applicationDbContext.Products.ToListAsync();
        return Ok(products);
    }

    // Get product by productCode
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(string productCode)
    {
        var product = await _applicationDbContext.Products.FirstOrDefaultAsync(e => e.ProductCode == productCode);
        if (product != null) return Ok(product);
        return NotFound();
    }

    // Add a new product
    [HttpPost]
    public async Task<ActionResult<List<Product>>> AddProduct(Product newProduct)
    {
        _applicationDbContext.Products.Add(newProduct);
        await _applicationDbContext.SaveChangesAsync();
        return Ok(newProduct);
    }

    // Delete a product
    [HttpDelete]
    public async Task<ActionResult<List<Product>>> DeleteProduct(string productCode)
    {
        var product = await _applicationDbContext.Products.FirstOrDefaultAsync(e => e.ProductCode == productCode);
        _applicationDbContext.Products.Remove(product);
        await _applicationDbContext.SaveChangesAsync();

        var products = await _applicationDbContext.Products.ToListAsync();
        return Ok(products);
    }


    [HttpPut]
    public async Task<ActionResult<Product>> UpdateProduct(Product updatedProduct)
    {
        var product =
            await _applicationDbContext.Products.FirstOrDefaultAsync(e => e.ProductCode == updatedProduct.ProductCode);

        if (product == null)
        {
            return NotFound($"Product with product code {updatedProduct.ProductCode} not found");
        }

        product.Name = updatedProduct.Name;
        await _applicationDbContext.SaveChangesAsync();

        var products = await _applicationDbContext.Products.ToListAsync();
        return Ok(products);
    }
}