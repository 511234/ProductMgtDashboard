using Microsoft.EntityFrameworkCore;
using ProductMgtDashboardBe.Models;

namespace ProductMgtDashboardBe.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}