using System.Data.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProductMgtDashboardBe.Controllers;
using ProductMgtDashboardBe.Data;
using ProductMgtDashboardBe.Models;

namespace ProductMgtDashboardBeTests.Controllers;

public class ProductControllerTest : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<ApplicationDbContext> _contextOptions;

    public ProductControllerTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new ApplicationDbContext(_contextOptions);
        context.Database.EnsureCreated();

        context.AddRange(
            new Product { ProductCode = "SD98A1I", Name = "Apples 6" },
            new Product { ProductCode = "K48AQ8Z", Name = "Chocolate bar 200g" });
        context.SaveChanges();
    }

    public void Dispose()
    {
        _connection.Close();
    }

    [Fact]
    public async Task GetProducts_Success_ReturnsAllProducts()
    {
        await using var context = new ApplicationDbContext(_contextOptions);
        var controller = new ProductController(context);

        var res = (await controller.GetProducts()).Result as OkObjectResult;
        res.Should().NotBeNull();
        var productList = res.Value as List<Product>;
        productList.Should().NotBeEmpty();
        productList.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetProductByProductCode_Fail_NotFound()
    {
        await using var context = new ApplicationDbContext(_contextOptions);
        var controller = new ProductController(context);

        var res = (await controller.GetProduct("abc")).Result;
        res.Should().NotBeNull();
        res.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetProductByProductCode_Success_ReturnsProduct()
    {
        await using var context = new ApplicationDbContext(_contextOptions);
        var controller = new ProductController(context);

        var res = (await controller.GetProduct("K48AQ8Z")).Result as OkObjectResult;
        res.Should().NotBeNull();
        var product = res.Value as Product;
        product.ProductCode.Should().Be("K48AQ8Z");
        product.Name.Should().Be("Chocolate bar 200g");
    }

    [Fact]
    public async Task GetProductQuantityAllCategories_Success_ReturnsProduct()
    {
        await using var context = new ApplicationDbContext(_contextOptions);
        var controller = new ProductController(context);

        var res = (await controller.GetProductQuantityAllCategories()).Result as OkObjectResult;
        res.Should().NotBeNull();
    }

    [Fact]
    public async Task AddProduct_Success()
    {
        await using var context = new ApplicationDbContext(_contextOptions);
        var controller = new ProductController(context);

        var newProduct = new Product
        {
            ProductCode = "D81JZ0A", Name = "Herbal Tea 20 bags", Price = 3, StockQuantity = 54,
            Category = ProductCategory.Food
        };
        var res = (await controller.AddProduct(newProduct)).Result as OkObjectResult;
        res.Should().NotBeNull();
        var product = res.Value as Product;
        product.ProductCode.Should().Be("D81JZ0A");
        product.Name.Should().Be("Herbal Tea 20 bags");
    }

    [Fact]
    public async Task AddProduct_Fail_DuplicateProductCode()
    {
        await using var context = new ApplicationDbContext(_contextOptions);
        var controller = new ProductController(context);

        var newProduct = new Product
        {
            ProductCode = "SD98A1I", Name = "Herbal Tea 20 bags", Price = 3, StockQuantity = 54,
            Category = ProductCategory.Food
        };
        var res = (await controller.AddProduct(newProduct)).Result;
        res.Should().BeOfType<UnprocessableEntityObjectResult>();
    }

    [Fact]
    public async Task UpdateProduct_Success()
    {
        await using var context = new ApplicationDbContext(_contextOptions);
        var controller = new ProductController(context);

        var updateProduct = new Product
        {
            ProductCode = "K48AQ8Z", Name = "Chocolate bar 200g", Price = 2, StockQuantity = 39,
            Category = ProductCategory.Food
        };
        var res = (await controller.UpdateProduct(updateProduct)).Result;
        res.Should().BeOfType<OkObjectResult>();
        var product = (res as OkObjectResult).Value as Product;
        product.Price.Should().Be(2);
        product.StockQuantity.Should().Be(39);
    }

    [Fact]
    public async Task DeleteProduct_Success()
    {
        await using var context = new ApplicationDbContext(_contextOptions);
        var controller = new ProductController(context);
        var res = (await controller.DeleteProduct("SD98A1I")).Result;
        res.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteProduct_Fail_NotFound()
    {
        await using var context = new ApplicationDbContext(_contextOptions);
        var controller = new ProductController(context);
        var res = (await controller.DeleteProduct("abbw1")).Result;
        res.Should().BeOfType<NotFoundObjectResult>();
    }
}