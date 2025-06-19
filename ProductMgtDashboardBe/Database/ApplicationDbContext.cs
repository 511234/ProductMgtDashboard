using Microsoft.EntityFrameworkCore;
using ProductMgtDashboardBe.Models.Entities;

namespace ProductMgtDashboardBe.Database;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }

}